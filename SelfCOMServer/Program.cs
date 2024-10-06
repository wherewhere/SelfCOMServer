using SelfCOMServer.Common;
using SelfCOMServer.Metadata;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading;
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
        public string SayHello() => ToString();

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
        private static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
            {
                ManualResetEvent _comServerExitEvent = new(false);
                _comServerExitEvent.Reset();
                PInvoke.CoRegisterClassObject(
                    new Guid("01153FC5-2F29-4F60-93AD-EFFB97CC9E20"),
                    new Factory<RemoteThing, IRemoteThing>(),
                    CLSCTX.CLSCTX_LOCAL_SERVER,
                    REGCLS.REGCLS_MULTIPLEUSE | REGCLS.REGCLS_SUSPENDED,
                    out uint cookie);
                PInvoke.CoResumeClassObjects();
                _comServerExitEvent.WaitOne();
                PInvoke.CoRevokeClassObject(cookie);
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
    }
}
