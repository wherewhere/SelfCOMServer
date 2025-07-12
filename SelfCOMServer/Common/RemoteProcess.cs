using SelfCOMServer.Metadata;
using System;
using System.ComponentModel;
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

        /// <inheritdoc cref="Process.StandardError"/>
        public ITextReader StandardError => new RemoteTextReader(target.StandardError);

        /// <inheritdoc cref="Process.ProcessName"/>
        public ITextWriter StandardInput => new RemoteTextWriter(target.StandardInput);

        /// <inheritdoc cref="Process.StandardOutput"/>
        public ITextReader StandardOutput => new RemoteTextReader(target.StandardOutput);

        /// <inheritdoc cref="Process.StartInfo"/>
        public IProcessStartInfo StartInfo
        {
            get => new RemoteProcessStartInfo(target.StartInfo);
            set => value.ToProcessStartInfo();
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

        /// <inheritdoc cref="Component.Dispose"/>
        public void Dispose()
        {
            target.Dispose();
            GC.SuppressFinalize(this);
        }

        public override partial string ToString();
    }

    /// <inheritdoc cref="Process"/>
    public sealed partial class ProcessStatic : IProcessStatic
    {
        /// <inheritdoc cref="Process.GetProcesses()"/>
        public IProcess[] GetProcesses() => [.. Process.GetProcesses().Select(x => new RemoteProcess(x))];

        public IProcess Start(IProcessStartInfo startInfo) =>
            Process.Start(startInfo.ToProcessStartInfo()) is Process process
                ? new RemoteProcess(process) : null;
    }
}
