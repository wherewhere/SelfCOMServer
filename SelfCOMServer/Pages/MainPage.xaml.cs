using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SelfCOMServer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IProcess process;

        public MainPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IRemoteThing remote = Factory.CreateRemoteThing();
            process = remote.CreateProcessStatic().Start(new RemoteProcessStartInfo
            {
                FileName = "cmd",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            AppTitle.Text = process.ProcessName;
            _ = ReadLinesAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            _ = SendCommandAsync(Input.Text);

        private IAsyncAction SendCommandAsync(string command) =>
            process.StandardInput.WriteAsync($"{command}{Environment.NewLine}");

        private async Task ReadLinesAsync()
        {
            ITextReader reader = process.StandardOutput;
            while (reader.Peek() >= 0)
            {
                await ThreadSwitcher.ResumeBackgroundAsync();
                string line = await reader.ReadLineAsync();
                await Dispatcher.ResumeForegroundAsync();
                Message.Blocks.Add(new Paragraph
                {
                    Inlines =
                    {
                        new Run { Text = line }
                    }
                });
                ScrollViewer.ChangeView(null, ScrollViewer.ScrollableHeight, null);
            }
        }
    }
}
