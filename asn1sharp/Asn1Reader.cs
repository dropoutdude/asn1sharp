using System.Threading;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Asn1Reader
    {
        public static async Task<Node> Parse(this ReaderSource readerSource)
        {
            return await readerSource.Parse(CancellationToken.None);
        }

        public static async Task<Node> Parse(this ReaderSource readerSource, CancellationToken token)
        {
            readerSource.Require(s => s.HasNextChunk(), "Source already exhausted!");

            var description = await Description(readerSource, token).ConfigureAwait(false);

            description.Require(d => d.IsConstructed, "Invalid root node!");

            return Node.From(description);
        }

        private static async Task<NodeDescription> Description(ReaderSource readerSource, CancellationToken token)
        {
            var chunk = await readerSource.NextChunk(token).ConfigureAwait(false);

            while (readerSource.HasNextChunk())
            {
                var next = await readerSource.NextChunk(token).ConfigureAwait(false);

                chunk = chunk.Combine(next);
            }

            var result = new StreamedResult(chunk);

            return result.AsDescription();
        }
    }
}
