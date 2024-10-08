using SelfCOMServer.Metadata;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SelfCOMServer.Common
{
    /// <inheritdoc cref="Process"/>
    [ComVisible(true)]
    [ComDefaultInterface(typeof(IProcess))]
    [GeneratedComClass]
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
    [ComVisible(true)]
    [ComDefaultInterface(typeof(IProcessStatic))]
    [GeneratedComClass]
    public sealed partial class ProcessStatic : IProcessStatic
    {
        /// <inheritdoc cref="Process.GetProcesses()"/>
        public IProcess[] GetProcesses() => Process.GetProcesses().Select(x => new RemoteProcess(x)).ToArray();

        public IProcess Start(IProcessStartInfo startInfo) =>
            Process.Start(startInfo.ToProcessStartInfo()) is Process process
                ? new RemoteProcess(process) : null;
    }
}
