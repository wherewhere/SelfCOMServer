using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            IRemoteThing remote = Factory.CreateRemoteThing();
            ListView.ItemsSource = remote.GetProcesses().Select(x => x.ToString()).ToArray();
        }
    }
}
