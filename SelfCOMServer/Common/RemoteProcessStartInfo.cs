using SelfCOMServer.Metadata;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SelfCOMServer.Common
{
    /// <inheritdoc cref="ProcessStartInfo"/>
    [ComVisible(true)]
    [ComDefaultInterface(typeof(IProcessStartInfo))]
    [GeneratedComClass]
    public sealed partial class RemoteProcessStartInfo(ProcessStartInfo inner) : IProcessStartInfo
    {
        public RemoteProcessStartInfo() : this(new ProcessStartInfo()) { }

        /// <inheritdoc cref="ProcessStartInfo.Argument"/>
        public IList<string> Argument => new CollectionVector<string>(inner.ArgumentList);

        /// <inheritdoc cref="ProcessStartInfo.Arguments"/>
        public string Arguments
        {
            get => inner.Arguments;
            set => inner.Arguments = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.CreateNoWindow"/>
        public bool CreateNoWindow
        {
            get => inner.CreateNoWindow;
            set => inner.CreateNoWindow = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.FileName"/>
        public string FileName
        {
            get => inner.FileName;
            set => inner.FileName = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.RedirectStandardError"/>
        public bool RedirectStandardError
        {
            get => inner.RedirectStandardError;
            set => inner.RedirectStandardError = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.RedirectStandardInput"/>
        public bool RedirectStandardInput
        {
            get => inner.RedirectStandardInput;
            set => inner.RedirectStandardInput = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.RedirectStandardOutput"/>
        public bool RedirectStandardOutput
        {
            get => inner.RedirectStandardOutput;
            set => inner.RedirectStandardOutput = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.UseShellExecute"/>
        public bool UseShellExecute
        {
            get => inner.UseShellExecute;
            set => inner.UseShellExecute = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.Verb"/>
        public string Verb
        {
            get => inner.Verb;
            set => inner.Verb = value;
        }

        /// <inheritdoc cref="ProcessStartInfo.Verbs"/>
        public string[] Verbs => inner.Verbs;

        public CoProcessWindowStyle WindowStyle
        {
            get => (CoProcessWindowStyle)inner.WindowStyle;
            set => inner.WindowStyle = (ProcessWindowStyle)value;
        }

        /// <inheritdoc cref="ProcessStartInfo.WorkingDirectory"/>
        public string WorkingDirectory
        {
            get => inner.WorkingDirectory;
            set => inner.WorkingDirectory = value;
        }

        public ProcessStartInfo ToProcessStartInfo() => inner;
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
