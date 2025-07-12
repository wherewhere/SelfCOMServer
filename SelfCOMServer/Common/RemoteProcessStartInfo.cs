using SelfCOMServer.Metadata;
using System.Collections.Generic;
using System.Diagnostics;
using WinRTWrapper.CodeAnalysis;

namespace SelfCOMServer.Common
{
    [WinRTWrapperMarshaller(typeof(ProcessStartInfo), typeof(IProcessStartInfo))]
    [GenerateWinRTWrapper(typeof(ProcessStartInfo), GenerateMember.Defined)]
    public sealed partial class RemoteProcessStartInfo : IProcessStartInfo
    {
        /// <inheritdoc cref="ProcessStartInfo()"/>
        public RemoteProcessStartInfo() : this(new ProcessStartInfo()) { }

        /// <inheritdoc cref="ProcessStartInfo.Argument"/>
        public IList<string> Argument => target.ArgumentList.AsVector();

        public partial string Arguments { get; set; }
        public partial bool CreateNoWindow { get; set; }
        public partial string FileName { get; set; }
        public partial bool RedirectStandardError { get; set; }
        public partial bool RedirectStandardInput { get; set; }
        public partial bool RedirectStandardOutput { get; set; }
        public partial bool UseShellExecute { get; set; }
        public partial string Verb { get; set; }
        public partial string[] Verbs { get; }

        /// <inheritdoc cref="ProcessStartInfo.WindowStyle"/>
        public CoProcessWindowStyle WindowStyle
        {
            get => (CoProcessWindowStyle)target.WindowStyle;
            set => target.WindowStyle = (ProcessWindowStyle)value;
        }

        public partial string WorkingDirectory { get; set; }

        /// <inheritdoc cref="ConvertToManaged(IProcessStartInfo)"/>
        public ProcessStartInfo ToProcessStartInfo() => target;
    }

    public static class ProcessStartInfoExtensions
    {
        public static ProcessStartInfo ToProcessStartInfo(this IProcessStartInfo startInfo) =>
            startInfo is RemoteProcessStartInfo remoteStartInfo
                ? remoteStartInfo.ToProcessStartInfo()
                : new ProcessStartInfo
                {
                    Arguments = startInfo.Arguments,
                    CreateNoWindow = startInfo.CreateNoWindow,
                    FileName = startInfo.FileName,
                    RedirectStandardError = startInfo.RedirectStandardError,
                    RedirectStandardInput = startInfo.RedirectStandardInput,
                    RedirectStandardOutput = startInfo.RedirectStandardOutput,
                    UseShellExecute = startInfo.UseShellExecute,
                    Verb = startInfo.Verb,
                    WindowStyle = (ProcessWindowStyle)startInfo.WindowStyle,
                    WorkingDirectory = startInfo.WorkingDirectory
                };
    }
}
