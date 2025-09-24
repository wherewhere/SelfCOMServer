using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;

namespace SelfCOMServer
{
    /// <summary>
    /// The manage class for remote object.
    /// </summary>
    public sealed partial class RemoteThing : IRemoteThing
    {
        private bool disposed;
        private RemoteMonitor _monitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteThing"/> class.
        /// </summary>
        public RemoteThing() => Program.RefCount++;

        /// <summary>
        /// Finalizes the instance of the <see cref="RemoteThing"/> class.
        /// </summary>
        ~RemoteThing() => Dispose();

        /// <inheritdoc cref="ProcessStatic.Instance"/>
        public IProcessStatic ProcessStatic => Common.ProcessStatic.Instance;

        /// <summary>
        /// Sets the monitor to check if the remote object is alive.
        /// </summary>
        /// <param name="handler">The handler to check if the remote object is alive.</param>
        /// <param name="period">The period to check if the remote object is alive.</param>
        public void SetMonitor(IsAliveHandler handler, TimeSpan period)
        {
            _monitor?.Stop();
            if (period.TotalMilliseconds > 0)
            {
                _monitor = new RemoteMonitor(handler, Dispose, period);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                _monitor?.Dispose();
                GC.SuppressFinalize(this);
                if (--Program.RefCount == 0)
                {
                    _ = Program.CheckReferenceAsync();
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString() =>
            new StringBuilder()
                .AppendLine("Information")
                .AppendLine($"Framework: {RuntimeInformation.FrameworkDescription}")
                .AppendLine($"OSPlatform: {Environment.OSVersion}")
                .Append($"OSArchitecture: {RuntimeInformation.OSArchitecture}")
                .ToString();
    }

    public static partial class Program
    {
        private const int RO_INIT_MULTITHREADED = 1;
        private static ManualResetEventSlim comServerExitEvent;

        public static int RefCount { get; set; }

        private static void Main(string[] args)
        {
            switch (args)
            {
                case ["-RegisterProcessAsComServer", ..]:
                    comServerExitEvent = new ManualResetEventSlim(false);
                    comServerExitEvent.Reset();
                    RemoteThingFactory factory = new();
                    factory.RegisterClassObject();
                    _ = CheckReferenceAsync();
                    comServerExitEvent.Wait();
                    factory.RevokeClassObject();
                    break;
                case ["-RegisterProcessAsWinRTServer", ..]:
                    _ = RoInitialize(RO_INIT_MULTITHREADED);
                    comServerExitEvent = new ManualResetEventSlim(false);
                    comServerExitEvent.Reset();
                    factory = new RemoteThingFactory();
                    factory.RegisterActivationFactory();
                    _ = CheckReferenceAsync();
                    comServerExitEvent.Wait();
                    factory.RevokeActivationFactory();
                    break;
                default:
                    Application.Start(static p =>
                    {
                        DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
                        SynchronizationContext.SetSynchronizationContext(context);
                        _ = new App();
                    });
                    break;
            }
        }

        public static async Task CheckReferenceAsync()
        {
            await Task.Delay(100);
            if (RefCount == 0)
            {
                comServerExitEvent?.Set();
            }
        }

        [LibraryImport("api-ms-win-core-winrt-l1-1-0.dll")]
        private static partial int RoInitialize(int initType);
    }
}
