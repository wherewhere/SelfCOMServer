using SelfCOMServer.Metadata;
using System.Runtime.CompilerServices;

namespace SelfCOMServer.Common
{
    public static class RemoteExtensions
    {
        extension(IProcess)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static IProcess Start(IProcessStartInfo startInfo) => Factory.CreateRemoteThing().ProcessStatic.Start(startInfo);
        }
    }
}
