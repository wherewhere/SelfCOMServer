#nullable enable

using Microsoft.Win32.SafeHandles;
using SelfCOMServer.Metadata;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace SelfCOMServer.Common
{
    [ComVisible(true)]
    [ComDefaultInterface(typeof(IRemoteProcess))]
    [GeneratedComClass]
    public sealed partial class RemoteProcess(Process process) : IRemoteProcess
    {
        /// <inheritdoc cref="Process.PriorityBoostEnabled"/>
        public bool PriorityBoostEnabled
        {
            get => process.PriorityBoostEnabled;
            set => process.PriorityBoostEnabled = value;
        }

        /// <inheritdoc cref="Process.PeakWorkingSet64"/>
        public long PeakWorkingSet64 => process.PeakWorkingSet64;

        /// <inheritdoc cref="Process.PeakWorkingSet"/>
        [Obsolete("Process.PeakWorkingSet has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PeakWorkingSet64 instead.")]
        public int PeakWorkingSet => process.PeakWorkingSet;

        /// <inheritdoc cref="Process.PeakVirtualMemorySize64"/>
        public long PeakVirtualMemorySize64 => process.PeakVirtualMemorySize64;

        /// <inheritdoc cref="Process.PeakVirtualMemorySize"/>
        [Obsolete("Process.PeakVirtualMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PeakVirtualMemorySize64 instead.")]
        public int PeakVirtualMemorySize => process.PeakVirtualMemorySize;

        /// <inheritdoc cref="Process.PeakPagedMemorySize64"/>
        public long PeakPagedMemorySize64 => process.PeakPagedMemorySize64;

        /// <inheritdoc cref="Process.PeakPagedMemorySize"/>
        [Obsolete("Process.PeakPagedMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PeakPagedMemorySize64 instead.")]
        public int PeakPagedMemorySize => process.PeakPagedMemorySize;

        /// <inheritdoc cref="Process.PagedSystemMemorySize64"/>
        public long PagedSystemMemorySize64 => process.PagedSystemMemorySize64;

        /// <inheritdoc cref="Process.PriorityClass"/>
        public ProcessPriorityClass PriorityClass => process.PriorityClass;

        /// <inheritdoc cref="Process.PagedSystemMemorySize"/>
        [Obsolete("Process.PagedSystemMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PagedSystemMemorySize64 instead.")]
        public int PagedSystemMemorySize => process.PagedSystemMemorySize;

        /// <inheritdoc cref="Process.PagedMemorySize"/>
        [Obsolete("Process.PagedMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PagedMemorySize64 instead.")]
        public int PagedMemorySize => process.PagedMemorySize;

        /// <inheritdoc cref="Process.NonpagedSystemMemorySize64"/>
        public long NonpagedSystemMemorySize64 => process.NonpagedSystemMemorySize64;

        /// <inheritdoc cref="Process.NonpagedSystemMemorySize"/>
        [Obsolete("Process.NonpagedSystemMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.NonpagedSystemMemorySize64 instead.")]
        public int NonpagedSystemMemorySize => process.NonpagedSystemMemorySize;

        /// <inheritdoc cref="Process.Modules"/>
        public ProcessModuleCollection Modules => process.Modules;

        /// <inheritdoc cref="Process.MinWorkingSet"/>
        public nint MinWorkingSet => process.MinWorkingSet;

        /// <inheritdoc cref="Process.MaxWorkingSet"/>
        public nint MaxWorkingSet => process.MaxWorkingSet;

        /// <inheritdoc cref="Process.MainWindowTitle"/>
        public string MainWindowTitle => process.MainWindowTitle;

        /// <inheritdoc cref="Process.MainWindowHandle"/>
        public nint MainWindowHandle => process.MainWindowHandle;

        /// <inheritdoc cref="Process.PagedMemorySize64"/>
        public long PagedMemorySize64 => process.PagedMemorySize64;

        /// <inheritdoc cref="Process.MainModule"/>
        public ProcessModule? MainModule => process.MainModule;

        /// <inheritdoc cref="Process.PrivateMemorySize"/>
        [Obsolete("Process.PrivateMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.PrivateMemorySize64 instead.")]
        public int PrivateMemorySize => process.PrivateMemorySize;

        /// <inheritdoc cref="Process.PrivilegedProcessorTime"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public TimeSpan PrivilegedProcessorTime => process.PrivilegedProcessorTime;

        /// <inheritdoc cref="Process.WorkingSet64"/>
        public long WorkingSet64 => process.WorkingSet64;

        /// <inheritdoc cref="Process.WorkingSet"/>
        [Obsolete("Process.WorkingSet has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.WorkingSet64 instead.")]
        public int WorkingSet => process.WorkingSet;

        /// <inheritdoc cref="Process.VirtualMemorySize64"/>
        public long VirtualMemorySize64 => process.VirtualMemorySize64;

        /// <inheritdoc cref="Process.VirtualMemorySize"/>
        [Obsolete("Process.VirtualMemorySize has been deprecated because the type of the property can't represent all valid results. Use System.Diagnostics.Process.VirtualMemorySize64 instead.")]
        public int VirtualMemorySize => process.VirtualMemorySize;

        /// <inheritdoc cref="Process.UserProcessorTime"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public TimeSpan UserProcessorTime => process.UserProcessorTime;

        /// <inheritdoc cref="Process.TotalProcessorTime"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public TimeSpan TotalProcessorTime => process.TotalProcessorTime;

        /// <inheritdoc cref="Process.Threads"/>
        public ProcessThreadCollection Threads => process.Threads;

        /// <inheritdoc cref="Process.SynchronizingObject"/>
        public ISynchronizeInvoke? SynchronizingObject
        {
            get => process.SynchronizingObject;
            set => process.SynchronizingObject = value;
        }

        /// <inheritdoc cref="Process.PrivateMemorySize64"/>
        public long PrivateMemorySize64 => process.PrivateMemorySize64;

        /// <inheritdoc cref="Process.StartTime"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public DateTime StartTime => process.StartTime;

        /// <inheritdoc cref="Process.StandardOutput"/>
        public StreamReader StandardOutput => process.StandardOutput;

        /// <inheritdoc cref="Process.StandardInput"/>
        public StreamWriter StandardInput => process.StandardInput;

        /// <inheritdoc cref="Process.StandardError"/>
        public StreamReader StandardError => process.StandardError;

        /// <inheritdoc cref="Process.SessionId"/>
        public int SessionId => process.SessionId;

        /// <inheritdoc cref="Process.SafeHandle"/>
        public SafeProcessHandle SafeHandle => process.SafeHandle;

        /// <inheritdoc cref="Process.Responding"/>
        public bool Responding => process.Responding;

        /// <inheritdoc cref="Process.ProcessorAffinity"/>
        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("linux")]
        public nint ProcessorAffinity
        {
            get => process.ProcessorAffinity;
            set => process.ProcessorAffinity = value;
        }

        /// <inheritdoc cref="Process.ProcessName"/>
        public string ProcessName => process.ProcessName;

        /// <inheritdoc cref="Process.StartInfo"/>
        public ProcessStartInfo StartInfo
        {
            get => process.StartInfo;
            set => process.StartInfo = value;
        }

        /// <inheritdoc cref="Process.Id"/>
        public int Id => process.Id;

        /// <inheritdoc cref="Process.MachineName"/>
        public string MachineName => process.MachineName;

        /// <inheritdoc cref="Process.HandleCount"/>
        public int HandleCount => process.HandleCount;

        /// <inheritdoc cref="Process.Handle"/>
        public nint Handle => process.Handle;

        /// <inheritdoc cref="Process.ExitTime"/>
        public DateTime ExitTime => process.ExitTime;

        /// <inheritdoc cref="Process.ExitCode"/>
        public int ExitCode => process.ExitCode;

        /// <inheritdoc cref="Process.EnableRaisingEvents"/>
        public bool EnableRaisingEvents
        {
            get => process.EnableRaisingEvents;
            set => process.EnableRaisingEvents = value;
        }

        /// <inheritdoc cref="Process.BasePriority"/>
        public int BasePriority => process.BasePriority;

        /// <inheritdoc cref="Process.HasExited"/>
        public bool HasExited => process.HasExited;

        /// <inheritdoc cref="Process.BeginErrorReadLine"/>
        public void BeginErrorReadLine() => process.BeginErrorReadLine();

        /// <inheritdoc cref="Process.BeginOutputReadLine"/>
        public void BeginOutputReadLine() => process.BeginOutputReadLine();

        /// <inheritdoc cref="Process.CancelErrorRead"/>
        public void CancelErrorRead() => process.CancelErrorRead();

        /// <inheritdoc cref="Process.CancelOutputRead"/>
        public void CancelOutputRead() => process.CancelOutputRead();

        /// <inheritdoc cref="Process.Close"/>
        public void Close() => process.Close();

        /// <inheritdoc cref="Process.CloseMainWindow"/>
        public bool CloseMainWindow() => process.CloseMainWindow();

        /// <inheritdoc cref="Process.Kill(bool)"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public void Kill(bool entireProcessTree) => process.Kill(entireProcessTree);

        /// <inheritdoc cref="Process.Kill"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public void Kill() => process.Kill();

        /// <inheritdoc cref="Process.Refresh"/>
        public void Refresh() => process.Refresh();

        /// <inheritdoc cref="Process.Start"/>
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public bool Start() => process.Start();

        /// <inheritdoc cref="Process.ToString"/>
        public override string ToString() => process.ToString();

        /// <inheritdoc cref="Process.WaitForExit"/>
        public void WaitForExit() => process.WaitForExit();

        /// <inheritdoc cref="Process.WaitForExit(int)"/>
        public bool WaitForExit(int milliseconds) => process.WaitForExit(milliseconds);

        /// <inheritdoc cref="Process.WaitForExit(TimeSpan)"/>
        public bool WaitForExit(TimeSpan timeout) => process.WaitForExit(timeout);

        /// <inheritdoc cref="Process.WaitForExitAsync(CancellationToken)"/>
        public Task WaitForExitAsync(CancellationToken cancellationToken = default) => process.WaitForExitAsync(cancellationToken);

        /// <inheritdoc cref="Process.WaitForInputIdle(int)"/>
        public bool WaitForInputIdle(int milliseconds) => process.WaitForInputIdle(milliseconds);

        /// <inheritdoc cref="Process.WaitForInputIdle(TimeSpan)"/>
        public bool WaitForInputIdle(TimeSpan timeout) => process.WaitForInputIdle(timeout);

        /// <inheritdoc cref="Process.WaitForInputIdle"/>
        public bool WaitForInputIdle() => process.WaitForInputIdle();

        /// <inheritdoc cref="Component.Dispose"/>
        public void Dispose() => process.Dispose();
    }
}
