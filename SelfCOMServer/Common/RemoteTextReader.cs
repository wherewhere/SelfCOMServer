using SelfCOMServer.Metadata;
using System;
using System.IO;
using Windows.Foundation;

namespace SelfCOMServer.Common
{
    /// <inheritdoc cref="TextReader"/>
    public partial class RemoteTextReader(TextReader inner) : ITextReader
    {
        /// <inheritdoc cref="TextReader.Peek"/>
        public int Peek() => inner.Peek();

        /// <inheritdoc cref="TextReader.ReadLineAsync"/>
        public IAsyncOperation<string> ReadLineAsync() => inner.ReadLineAsync().AsAsyncOperation();

        /// <inheritdoc cref="TextReader.ReadToEndAsync"/>
        public IAsyncOperation<string> ReadToEndAsync() => inner.ReadToEndAsync().AsAsyncOperation();

        /// <inheritdoc cref="TextReader.Dispose"/>
        public void Dispose()
        {
            inner.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
