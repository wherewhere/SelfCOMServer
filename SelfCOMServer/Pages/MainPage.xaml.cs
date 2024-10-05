using System;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SelfCOMServer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly static Guid CLSID_IUnknown = new("00000000-0000-0000-C000-000000000046");

        public MainPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IStringable a = CreateInstance<IStringable>(new Guid("01153FC5-2F29-4F60-93AD-EFFB97CC9E20"), CLSCTX.CLSCTX_ALL);
            Message.Text = a.ToString();
        }

        internal static T CreateInstance<T>(Guid rclsid, CLSCTX dwClsContext = CLSCTX.CLSCTX_INPROC_SERVER)
        {
            Guid riid = CLSID_IUnknown;
            HRESULT hresult = PInvoke.CoCreateInstance(rclsid, null, dwClsContext, riid, out object result);
            if (hresult.Failed)
            {
                hresult.ThrowOnFailure();
            }
            nint pUnknown = Marshal.GetIUnknownForObject(result);
            return Marshaler<T>.FromAbi(pUnknown);
        }
    }
}
