using SelfCOMServer.Metadata;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Windows.Foundation;

namespace SelfCOMServer.Common
{
    /// <inheritdoc cref="TextWriter"/>
    [ComVisible(true)]
    [ComDefaultInterface(typeof(ITextWriter))]
    [GeneratedComClass]
    public partial class RemoteTextWriter(TextWriter inner) : ITextWriter
    {
        /// <inheritdoc cref="TextWriter.FlushAsync"/>
        public IAsyncAction FlushAsync() => inner.FlushAsync().AsAsyncAction();

        /// <inheritdoc cref="TextWriter.WriteAsync(string?)"/>
        public IAsyncAction WriteAsync(string value) => inner.WriteAsync(value).AsAsyncAction();

        /// <inheritdoc cref="TextWriter.Dispose"/>
        public void Dispose()
        {
            inner.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="TextWriter.DisposeAsync"/>
        public IAsyncAction DisposeAsync() => inner.DisposeAsync().AsTask().AsAsyncAction();
    }
}
