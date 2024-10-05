using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WinRT;
using WinRT.Interop;

namespace SelfCOMServer.Common
{
    [ComVisible(true)]
    [GeneratedComClass]
    public partial class Factory<T, TInterface> : IActivationFactory, IClassFactory where T : TInterface, new()
    {
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        private static readonly Guid CLSID_Unknown = new("00000000-0000-0000-C000-000000000046");

        public nint ActivateInstance() => MarshalInspectable<TInterface>.FromManaged(new T());

        public void CreateInstance(
            [MarshalAs(UnmanagedType.Interface)] object pUnkOuter,
            ref Guid riid,
            out IntPtr ppvObject)
        {
            ppvObject = IntPtr.Zero;

            if (pUnkOuter != null)
            {
                Marshal.ThrowExceptionForHR(-2147221232);
            }

            if (riid == typeof(TInterface).GUID || riid == CLSID_Unknown)
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
    }

#pragma warning disable SYSLIB1096
    // https://docs.microsoft.com/windows/win32/api/unknwn/nn-unknwn-iclassfactory
    [ComImport]
    [ComVisible(false)]
    [GeneratedComInterface]
    [Guid("00000001-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IClassFactory
    {
        void CreateInstance(
            [MarshalAs(UnmanagedType.Interface)] object pUnkOuter,
            ref Guid riid,
            out nint ppvObject);

        void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock);
    }
#pragma warning restore SYSLIB1096
}
