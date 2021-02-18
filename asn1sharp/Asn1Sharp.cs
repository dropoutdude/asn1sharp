using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Asn1Sharp
    {
        public static async Task<Node> ParseBinary(this byte[] bytes)
        {
            return await bytes.ParseBinary(CancellationToken.None);
        }

        public static async Task<Node> ParseBinary(this byte[] bytes, CancellationToken token)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var source = new BinarySource(stream);

                return await source.Parse(token).ConfigureAwait(false);
            }
        }

        public static async Task<Node> ParsePem(this Stream stream)
        {
            return await stream.ParsePem(CancellationToken.None);
        }

        public static async Task<Node> ParsePem(this Stream stream, CancellationToken token)
        {
            using (var source = new PemSource(stream))
            {
                return await source.Parse(token).ConfigureAwait(false);
            }
        }
    }
}
