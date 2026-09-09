using SelfCOMServer.Metadata;
using System.IO;
using Windows.Foundation;
using WinRTWrapper.CodeAnalysis;

namespace SelfCOMServer.Common
{
    [WinRTWrapperMarshaller(typeof(TextReader), typeof(ITextReader))]
    [GenerateWinRTWrapper(typeof(TextReader), GenerateMember.Defined)]
    public sealed partial class RemoteTextReader : ITextReader
    {
        public partial int Peek();
        public partial IAsyncOperation<string> ReadLineAsync();
        public partial IAsyncOperation<string> ReadToEndAsync();
        public partial void Dispose();
    }
}
