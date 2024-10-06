using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Win32.System.Com;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SelfCOMServer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IRemoteThing a = Factory.CreateInstance<IRemoteThing>(new Guid("01153FC5-2F29-4F60-93AD-EFFB97CC9E20"), CLSCTX.CLSCTX_ALL);
            Message.Text = a.SayHello();
        }
    }
}
