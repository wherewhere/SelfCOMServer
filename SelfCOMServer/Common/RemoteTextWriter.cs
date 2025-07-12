using SelfCOMServer.Metadata;
using System.IO;
using Windows.Foundation;
using WinRTWrapper.CodeAnalysis;

namespace SelfCOMServer.Common
{
    [WinRTWrapperMarshaller(typeof(TextWriter), typeof(ITextWriter))]
    [GenerateWinRTWrapper(typeof(TextWriter), GenerateMember.Defined)]
    public partial class RemoteTextWriter : ITextWriter
    {
        public partial IAsyncAction FlushAsync();
        public partial IAsyncAction WriteAsync(string value);
        public partial void Dispose();
        public partial IAsyncAction DisposeAsync();
    }
}
