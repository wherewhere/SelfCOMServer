using SelfCOMServer.Metadata;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Threading;
using Windows.Win32.System.Com;
using WinRT;
using WinRT.Interop;

namespace SelfCOMServer.Common
{
    [SuppressMessage("Interoperability", "SYSLIB1097:添加“GeneratedComClassAttribute”以启用将此类型的对象传递到 COM", Justification = "<挂起>")]
    public abstract partial class Factory<T, TInterface> : IActivationFactory, IClassFactory where T : TInterface, new()
    {
        private const int E_NOINTERFACE = unchecked((int)0x80004002);
        private const int CLASS_E_NOAGGREGATION = unchecked((int)0x80040110);

        private readonly Guid _iid = typeof(TInterface).GUID;

        protected uint co_cookie;
        protected nint ro_cookie;

        public nint ActivateInstance() => MarshalInspectable<TInterface>.FromManaged(new T());

        public void CreateInstance(nint pUnkOuter, in Guid riid, out nint ppvObject)
        {
            ppvObject = 0;

            if (pUnkOuter != 0)
            {
                Marshal.ThrowExceptionForHR(CLASS_E_NOAGGREGATION);
            }

            if (riid == _iid || riid == Factory.CLSID_IUnknown)
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

        public void RevokeClassObject()
        {
            int hresult = Factory.CoRevokeClassObject(co_cookie);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }

        public void RevokeActivationFactory() => Factory.RoRevokeActivationFactories(ro_cookie);

        public abstract void RegisterClassObject();
        public abstract void RegisterActivationFactory();
        protected abstract nint GetActivationFactory(nint activatableClassId, out nint factory);
    }

    [GeneratedComClass]
    public sealed partial class RemoteThingFactory : Factory<RemoteThing, IRemoteThing>
    {
        private const nint S_OK = 0;
        private const nint E_INVALIDARG = unchecked((int)0x80070057);

        public override void RegisterClassObject()
        {
            int hresult = CoRegisterClassObject(
                Factory.CLSID_IRemoteThing,
                this,
                (uint)CLSCTX.CLSCTX_LOCAL_SERVER,
                (int)REGCLS.REGCLS_MULTIPLEUSE,
                out co_cookie);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }

        public override void RegisterActivationFactory()
        {
            nint activatableClassId = MarshalString.FromManaged(Factory.ClassID_IRemoteThing);
            int hresult = RoRegisterActivationFactories([activatableClassId], [GetActivationFactory], 1, out ro_cookie);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }

        protected override nint GetActivationFactory(nint activatableClassId, out nint factory)
        {
            try
            {
                string _classId = MarshalString.FromAbi(activatableClassId);
                if (_classId == Factory.ClassID_IRemoteThing)
                {
                    factory = MarshalInterface<IActivationFactory>.FromManaged(this);
                    return S_OK;
                }
                factory = default;
                return E_INVALIDARG;
            }
            catch (Exception ex)
            {
                factory = default;
                return ExceptionHelpers.GetHRForException(ex);
            }
        }

        [LibraryImport("api-ms-win-core-com-l1-1-0.dll")]
        private static partial int CoRegisterClassObject(in Guid rclsid, IClassFactory pUnk, uint dwClsContext, int flags, out uint lpdwRegister);

        private delegate nint DllGetActivationFactory([In] nint activatableClassId, [Out] out nint factory);

        [LibraryImport("api-ms-win-core-winrt-l1-1-0.dll")]
        private static partial int RoRegisterActivationFactories([In] nint[] activatableClassIds, [In] DllGetActivationFactory[] activationFactoryCallbacks, uint count, out nint cookie);
    }

    public static partial class Factory
    {
        public static readonly Guid CLSID_IRemoteThing = new("01153FC5-2F29-4F60-93AD-EFFB97CC9E20");
        public static readonly Guid CLSID_IUnknown = new("00000000-0000-0000-C000-000000000046");

        public const string ClassID_IRemoteThing = "SelfCOMServer.Server";

        private static bool IsAlive() => true;

        public static IRemoteThing CreateRemoteThing() =>
            CreateInstance<IRemoteThing>(CLSID_IRemoteThing, CLSCTX.CLSCTX_ALL, TimeSpan.FromSeconds(30));

        internal static T CreateInstance<T>(in Guid rclsid, CLSCTX dwClsContext = CLSCTX.CLSCTX_INPROC_SERVER)
        {
            int hresult = CoCreateInstance(rclsid, 0, (uint)dwClsContext, CLSID_IUnknown, out nint result);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
            return Marshaler<T>.FromAbi(result);
        }

        internal static T CreateInstance<T>(in Guid rclsid, CLSCTX dwClsContext, in TimeSpan period) where T : ISetMonitor
        {
            T results = CreateInstance<T>(rclsid, dwClsContext);
            results.SetMonitor(IsAlive, period);
            return results;
        }

        public static IRemoteThing ActivateRemoteThing() =>
            ActivateInstance<IRemoteThing>(ClassID_IRemoteThing, TimeSpan.FromSeconds(30));

        internal static T ActivateInstance<T>(string activatableClassId)
        {
            nint classId = MarshalString.FromManaged(activatableClassId);
            int hresult = RoActivateInstance(classId, out nint instance);
            if (hresult < 0)
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
            return Marshaler<T>.FromAbi(instance);
        }

        internal static T ActivateInstance<T>(string activatableClassId, in TimeSpan period) where T : ISetMonitor
        {
            T result = ActivateInstance<T>(activatableClassId);
            result.SetMonitor(IsAlive, period);
            return result;
        }

        [LibraryImport("api-ms-win-core-com-l1-1-0.dll")]
        private static partial int CoCreateInstance(in Guid rclsid, nint pUnkOuter, uint dwClsContext, in Guid riid, out nint ppv);

        [LibraryImport("api-ms-win-core-winrt-l1-1-0.dll")]
        private static partial int RoActivateInstance(nint activatableClassId, out nint instance);

        [LibraryImport("api-ms-win-core-com-l1-1-0.dll")]
        internal static partial int CoRevokeClassObject(uint dwRegister);

        [LibraryImport("api-ms-win-core-winrt-l1-1-0.dll")]
        internal static partial void RoRevokeActivationFactories(nint cookie);
    }

    /// <summary>
    /// Represents a monitor that checks if a remote object is alive.
    /// </summary>
    public sealed partial class RemoteMonitor : IDisposable
    {
        private bool disposed;
        private readonly Timer _timer;
        private readonly Action _dispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteMonitor"/> class.
        /// </summary>
        /// <param name="handler">The handler to check if the remote object is alive.</param>
        /// <param name="dispose">The action to dispose the remote object.</param>
        /// <param name="period">The period to check if the remote object is alive.</param>
        public RemoteMonitor(IsAliveHandler handler, Action dispose, in TimeSpan period)
        {
            ArgumentNullException.ThrowIfNull(handler);
            ArgumentNullException.ThrowIfNull(dispose);
            _dispose = dispose;
            _timer = new(_ =>
            {
                bool isAlive = false;
                try
                {
                    isAlive = handler();
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

        /// <summary>
        /// Finalizes the instance of the <see cref="RemoteMonitor"/> class.
        /// </summary>
        ~RemoteMonitor() => Dispose();

        /// <summary>
        /// Stops the monitor.
        /// </summary>
        public void Stop()
        {
            if (!disposed)
            {
                disposed = true;
                _timer.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                _timer.Dispose();
                _dispose();
                GC.SuppressFinalize(this);
            }
        }
    }

    // https://docs.microsoft.com/windows/win32/api/unknwn/nn-unknwn-iclassfactory
    [GeneratedComInterface]
    [Guid("00000001-0000-0000-C000-000000000046")]
    public partial interface IClassFactory
    {
        void CreateInstance(nint pUnkOuter, in Guid riid, out nint ppvObject);

        void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
    }
}
