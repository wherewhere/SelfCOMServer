using SelfCOMServer.Metadata;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SelfCOMServer.Common
{
    /// <inheritdoc cref="Process"/>
    public partial class RemoteProcess(Process inner) : IProcess
    {
        /// <inheritdoc cref="Process.ProcessName"/>
        public string ProcessName => inner.ProcessName;

        /// <inheritdoc cref="Process.StandardError"/>
        public ITextReader StandardError => new RemoteTextReader(inner.StandardError);

        /// <inheritdoc cref="Process.ProcessName"/>
        public ITextWriter StandardInput => new RemoteTextWriter(inner.StandardInput);

        /// <inheritdoc cref="Process.StandardOutput"/>
        public ITextReader StandardOutput => new RemoteTextReader(inner.StandardOutput);

        /// <inheritdoc cref="Process.StartInfo"/>
        public IProcessStartInfo StartInfo
        {
            get => new RemoteProcessStartInfo(inner.StartInfo);
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
                inner.ErrorDataReceived += handler;
                errorDataReceived.Add(value, handler);
            }
            remove
            {
                if (errorDataReceived.TryGetValue(value, out DataReceivedEventHandler handler))
                {
                    inner.ErrorDataReceived -= handler;
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
                inner.OutputDataReceived += handler;
                outputDataReceived.Add(value, handler);
            }
            remove
            {
                if (outputDataReceived.TryGetValue(value, out DataReceivedEventHandler handler))
                {
                    inner.OutputDataReceived -= handler;
                    outputDataReceived.Remove(value);
                }
            }
        }

        /// <inheritdoc cref="Process.BeginErrorReadLine"/>
        public void BeginErrorReadLine() => inner.BeginErrorReadLine();

        /// <inheritdoc cref="Process.BeginOutputReadLine"/>
        public void BeginOutputReadLine() => inner.BeginOutputReadLine();

        /// <inheritdoc cref="Process.CancelErrorRead"/>
        public void CancelErrorRead() => inner.CancelErrorRead();

        /// <inheritdoc cref="Process.CancelOutputRead"/>
        public void CancelOutputRead() => inner.CancelOutputRead();

        /// <inheritdoc cref="Component.Dispose"/>
        public void Dispose()
        {
            inner.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="Process.ToString"/>
        public override string ToString() => inner.ToString();
    }

    /// <inheritdoc cref="Process"/>
    public sealed partial class ProcessStatic : IProcessStatic
    {
        /// <inheritdoc cref="Process.GetProcesses()"/>
        public IProcess[] GetProcesses() => Process.GetProcesses().Select(x => new RemoteProcess(x)).ToArray();

        public IProcess Start(IProcessStartInfo startInfo) =>
            Process.Start(startInfo.ToProcessStartInfo()) is Process process
                ? new RemoteProcess(process) : null;
    }
}
