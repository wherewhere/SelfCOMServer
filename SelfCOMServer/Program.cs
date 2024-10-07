using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.Win32;
using Windows.Win32.System.Com;

namespace SelfCOMServer
{
    [ComVisible(true)]
    [ComDefaultInterface(typeof(IRemoteThing))]
    [GeneratedComClass]
    public sealed partial class RemoteThing : IRemoteThing
    {
        private bool disposed;
        private RemoteMonitor _monitor;

        public RemoteThing() => Program.RefCount++;

        ~RemoteThing() => Dispose();

        public string SayHello() => ToString();

        public void SetMonitor(IsAliveHandler handler, TimeSpan period) => _monitor = new RemoteMonitor(handler, Dispose, period);

        public IEnumerable<IRemoteProcess> GetProcesses() => Process.GetProcesses().Select(x => new RemoteProcess(x));

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                _monitor?.Dispose();
                GC.SuppressFinalize(this);
                if (--Program.RefCount == 0)
                {
                    _ = Program.CheckComRefAsync();
                }
            }
        }

        public override string ToString() =>
            new StringBuilder()
                .AppendLine("Information")
                .AppendLine($"Framework: {RuntimeInformation.FrameworkDescription}")
                .AppendLine($"OSPlatform: {Environment.OSVersion}")
                .Append($"OSArchitecture: {RuntimeInformation.OSArchitecture}")
                .ToString();
    }

    public static class Program
    {
        private static ManualResetEventSlim comServerExitEvent;

        public static int RefCount { get; set; }

        private static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
            {
                comServerExitEvent = new ManualResetEventSlim(false);
                comServerExitEvent.Reset();
                _ = PInvoke.CoRegisterClassObject(
                    Factory.CLSID_IRemoteThing,
                    new Factory<RemoteThing, IRemoteThing>(),
                    CLSCTX.CLSCTX_LOCAL_SERVER,
                    REGCLS.REGCLS_MULTIPLEUSE,
                    out uint cookie);
                _ = CheckComRefAsync();
                comServerExitEvent.Wait();
                _ = PInvoke.CoRevokeClassObject(cookie);
            }
            else
            {
                Application.Start(static p =>
                {
                    DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(context);
                    _ = new App();
                });
            }
        }

        public static async Task CheckComRefAsync()
        {
            await Task.Delay(100);
            if (RefCount == 0)
            {
                comServerExitEvent?.Set();
            }
        }
    }
}
