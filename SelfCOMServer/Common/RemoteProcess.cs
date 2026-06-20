using SelfCOMServer.Metadata;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using WinRTWrapper.CodeAnalysis;

namespace SelfCOMServer.Common
{
    [WinRTWrapperMarshaller(typeof(Process), typeof(IProcess))]
    [GenerateWinRTWrapper(typeof(Process), GenerateMember.Defined)]
    public partial class RemoteProcess : IProcess
    {
        public partial string ProcessName { get; }
        [WinRTWrapperMarshalUsing(typeof(RemoteTextReader))]
        public partial ITextReader StandardOutput { get; }
        [WinRTWrapperMarshalUsing(typeof(RemoteTextWriter))]
        public partial ITextWriter StandardInput { get; }
        [WinRTWrapperMarshalUsing(typeof(RemoteTextReader))]
        public partial ITextReader StandardError { get; }
        [WinRTWrapperMarshalUsing(typeof(RemoteProcessStartInfo))]
        public partial IProcessStartInfo StartInfo { get; set; }
        public partial int ExitCode { get; }
        public partial bool EnableRaisingEvents { get; set; }

        /// <summary>
        /// The event weak table for the <see cref="Process.Exited"/> event.
        /// </summary>
        private readonly ConditionalWeakTable<EventHandler<IEventArgs>, EventHandler> exited = [];
        /// <inheritdoc cref="Process.Exited"/>
        public event EventHandler<IEventArgs> Exited
        {
            add
            {
                void wrapper(object sender, EventArgs e) => value(this, new RemoteEventArgs(e));
                EventHandler handler = wrapper;
                target.Exited += handler;
                exited.Add(value, handler);
            }
            remove
            {
                if (exited.TryGetValue(value, out EventHandler handler))
                {
                    target.Exited -= handler;
                    exited.Remove(value);
                }
            }
        }

        private readonly ConditionalWeakTable<CoDataReceivedEventHandler, DataReceivedEventHandler> errorDataReceived = [];
        /// <inheritdoc cref="Process.ErrorDataReceived"/>
        public event CoDataReceivedEventHandler ErrorDataReceived
        {
            add
            {
                void wrapper(object sender, DataReceivedEventArgs e) => value(this, new CoDataReceivedEventArgs(e.Data));
                DataReceivedEventHandler handler = wrapper;
                target.ErrorDataReceived += handler;
                errorDataReceived.Add(value, handler);
            }
            remove
            {
                if (errorDataReceived.TryGetValue(value, out DataReceivedEventHandler handler))
                {
                    target.ErrorDataReceived -= handler;
                    errorDataReceived.Remove(value);
                }
            }
        }

        private readonly ConditionalWeakTable<CoDataReceivedEventHandler, DataReceivedEventHandler> outputDataReceived = [];
        /// <inheritdoc cref="Process.OutputDataReceived"/>
        public event CoDataReceivedEventHandler OutputDataReceived
        {
            add
            {
                void wrapper(object sender, DataReceivedEventArgs e) => value(this, new CoDataReceivedEventArgs(e.Data));
                DataReceivedEventHandler handler = wrapper;
                target.OutputDataReceived += handler;
                outputDataReceived.Add(value, handler);
            }
            remove
            {
                if (outputDataReceived.TryGetValue(value, out DataReceivedEventHandler handler))
                {
                    target.OutputDataReceived -= handler;
                    outputDataReceived.Remove(value);
                }
            }
        }

        public partial void BeginErrorReadLine();
        public partial void BeginOutputReadLine();
        public partial void CancelErrorRead();
        public partial void CancelOutputRead();
        public partial void Dispose();
        public override partial string ToString();
    }

    /// <inheritdoc cref="Process"/>
    public sealed partial class ProcessStatic : IProcessStatic
    {
        /// <summary>
        /// Gets the singleton instance of the <see cref="ProcessStatic"/> class.
        /// </summary>
        public static ProcessStatic Instance { get; } = new();

        /// <inheritdoc cref="Process.GetProcesses()"/>
        public IProcess[] GetProcesses() => [.. Process.GetProcesses().Select(x => new RemoteProcess(x))];

        /// <inheritdoc cref="Process.Start(ProcessStartInfo)"/>
        public IProcess Start(IProcessStartInfo startInfo) =>
            Process.Start(RemoteProcessStartInfo.ConvertToManaged(startInfo)) is Process process
                ? new RemoteProcess(process) : null;
    }
}
