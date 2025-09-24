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

        /// <inheritdoc cref="ProcessStartInfo(string)"/>
        public RemoteProcessStartInfo(string fileName) : this(new ProcessStartInfo(fileName)) { }

        /// <inheritdoc cref="ProcessStartInfo(string, string)"/>
        public RemoteProcessStartInfo(string fileName, string arguments) : this(new ProcessStartInfo(fileName, arguments)) { }

        /// <inheritdoc cref="ProcessStartInfo(string, IEnumerable{string})"/>
        public RemoteProcessStartInfo(string fileName, IEnumerable<string> arguments) : this(new ProcessStartInfo(fileName, arguments)) { }

        public partial string[] Verbs { get; }
        public partial string Verb { get; set; }
        public partial bool UseShellExecute { get; set; }
        public partial string UserName { get; set; }
        public partial bool RedirectStandardOutput { get; set; }
        public partial bool RedirectStandardInput { get; set; }
        public partial bool RedirectStandardError { get; set; }

        /// <inheritdoc cref="ProcessStartInfo.WindowStyle"/>
        public CoProcessWindowStyle WindowStyle
        {
            get => (CoProcessWindowStyle)target.WindowStyle;
            set => target.WindowStyle = (ProcessWindowStyle)value;
        }

        public partial bool LoadUserProfile { get; set; }
        public partial string FileName { get; set; }
        public partial bool ErrorDialog { get; set; }
        public partial IDictionary<string, string> Environment { get; }
        public partial string Domain { get; set; }
        public partial bool CreateNoWindow { get; set; }
        public partial string Arguments { get; set; }

        /// <inheritdoc cref="ProcessStartInfo.Argument"/>
        public IList<string> ArgumentList => target.ArgumentList;

        public partial string WorkingDirectory { get; set; }

        /// <summary>
        /// Converts a wrapper type <see cref="IProcessStartInfo"/> to a managed type <see cref="ProcessStartInfo"/>.
        /// </summary>
        /// <param name="wrapper">The wrapper type to convert.</param>
        /// <returns>The converted managed type.</returns>
        public static ProcessStartInfo ConvertToManaged(IProcessStartInfo wrapper) =>
            wrapper is RemoteProcessStartInfo remoteStartInfo
                ? remoteStartInfo.target
                : new ProcessStartInfo
                {
                    Arguments = wrapper.Arguments,
                    CreateNoWindow = wrapper.CreateNoWindow,
                    FileName = wrapper.FileName,
                    RedirectStandardError = wrapper.RedirectStandardError,
                    RedirectStandardInput = wrapper.RedirectStandardInput,
                    RedirectStandardOutput = wrapper.RedirectStandardOutput,
                    UseShellExecute = wrapper.UseShellExecute,
                    Verb = wrapper.Verb,
                    WindowStyle = (ProcessWindowStyle)wrapper.WindowStyle,
                    WorkingDirectory = wrapper.WorkingDirectory
                };
    }
}
