using SelfCOMServer.Metadata;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Threading;
using Windows.Win32.System.Com;
using WinRT;
using WinRT.Interop;

namespace SelfCOMServer.Common
{
#pragma warning disable SYSLIB1097
    public abstract partial class Factory<T, TInterface> : IActivationFactory, IClassFactory where T : TInterface, new()
    {
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        public nint ActivateInstance() => MarshalInspectable<TInterface>.FromManaged(new T());

        public void CreateInstance(
            [MarshalAs(UnmanagedType.Interface)] object pUnkOuter,
            in Guid riid,
            out nint ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if (pUnkOuter != null)
            {
                Marshal.ThrowExceptionForHR(-2147221232);
            }

            if (riid == typeof(TInterface).GUID || riid == Factory.CLSID_IUnknown)
            {
                // Create the instance of the .NET object
                ppvObject = MarshalInspectable<TInterface>.FromManaged(new T());
            }
            else
            {
                // The object that ppvObject points to does not support the
                // interface identified by riid.
                Marshal.ThrowExceptionForHR(E_NOINTERFACE);
            }
        }

        public void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock)
        {
        }

        public abstract void RegisterClassObject();
        public abstract void RevokeClassObject();
    }
#pragma warning restore SYSLIB1097

    [GeneratedComClass]
    public partial class RemoteThingFactory : Factory<RemoteThing, IRemoteThing>
    {
        private uint cookie;

        public override void RegisterClassObject()
        {
            StrategyBasedComWrappers wrappers = new();
            int hresult = Factory.CoRegisterClassObject(
                Factory.CLSID_IRemoteThing,
                this,
                (uint)CLSCTX.CLSCTX_LOCAL_SERVER,
                (int)REGCLS.REGCLS_MULTIPLEUSE,
                out cookie);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }

        public override void RevokeClassObject()
        {
            int hresult = Factory.CoRevokeClassObject(cookie);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }
    }

    public static partial class Factory
    {
        public static readonly Guid CLSID_IRemoteThing = new("01153FC5-2F29-4F60-93AD-EFFB97CC9E20");
        public static readonly Guid CLSID_IUnknown = new("00000000-0000-0000-C000-000000000046");

        public static bool IsAlive() => true;

        public static IRemoteThing CreateRemoteThing() =>
            CreateInstance<IRemoteThing>(CLSID_IRemoteThing, CLSCTX.CLSCTX_ALL, TimeSpan.FromMinutes(1));

        internal static T CreateInstance<T>(Guid rclsid, CLSCTX dwClsContext = CLSCTX.CLSCTX_INPROC_SERVER)
        {
            Guid riid = CLSID_IUnknown;
            int hresult = CoCreateInstance(rclsid, 0, (uint)dwClsContext, riid, out nint result);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
            return Marshaler<T>.FromAbi(result);
        }

        internal static T CreateInstance<T>(Guid rclsid, CLSCTX dwClsContext, TimeSpan period) where T : ISetMonitor
        {
            T results = CreateInstance<T>(rclsid, dwClsContext);
            results.SetMonitor(IsAlive, period);
            return results;
        }

        [LibraryImport("ole32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static partial int CoCreateInstance(in Guid rclsid, nint pUnkOuter, uint dwClsContext, in Guid riid, out nint ppv);

        [LibraryImport("ole32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static partial int CoRegisterClassObject(in Guid rclsid, IClassFactory pUnk, uint dwClsContext, int flags, out uint lpdwRegister);

        [LibraryImport("ole32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static partial int CoRevokeClassObject(uint dwRegister);
    }

    public sealed partial class RemoteMonitor : IDisposable
    {
        private bool disposed;
        private readonly Timer _timer;
        private readonly Action _dispose;

        public RemoteMonitor(IsAliveHandler handler, Action dispose, TimeSpan period)
        {
            _dispose = dispose;
            _timer = new(_ =>
            {
                bool isAlive = false;
                try
                {
                    isAlive = handler.Invoke();
                }
                catch
                {
                    isAlive = false;
                }
                finally
                {
                    if (!isAlive)
                    {
                        Dispose();
                    }
                }
            }, null, TimeSpan.Zero, period);
        }

        ~RemoteMonitor() => Dispose();

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                _timer.Dispose();
                _dispose?.Invoke();
                GC.SuppressFinalize(this);
            }
        }
    }

    // https://docs.microsoft.com/windows/win32/api/unknwn/nn-unknwn-iclassfactory
    [GeneratedComInterface]
    [Guid("00000001-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IClassFactory
    {
        void CreateInstance(
            [MarshalAs(UnmanagedType.Interface)] object pUnkOuter,
            in Guid riid,
            out nint ppvObject);

        void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
    }
}
