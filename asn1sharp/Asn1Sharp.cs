using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Asn1Sharp
    {
        public static async Task<Node> ParseBinary(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                var source = new BinarySource(stream);

                return await source.Parse().ConfigureAwait(false);
            }
        }

        public static async Task<Node> ParsePem(this Stream stream)
        {
            using (var source = new PemSource(stream))
            {
                return await source.Parse().ConfigureAwait(false);
            }
        }
    }
}
