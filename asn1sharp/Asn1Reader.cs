using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Asn1Reader
    {
        public static async Task<Node> Parse(this ReaderSource readerSource)
        {
            readerSource.Require(s => s.HasNextChunk(), "Source already exhausted!");

            var description = await Description(readerSource).ConfigureAwait(false);

            description.Require(d => d.IsConstructed, "Invalid root node!");

            return Node.From(description);
        }

        private static async Task<NodeDescription> Description(ReaderSource readerSource)
        {
            var chunk = await readerSource.NextChunk().ConfigureAwait(false);

            while (readerSource.HasNextChunk())
            {
                var next = await readerSource.NextChunk().ConfigureAwait(false);

                chunk = chunk.Combine(next);
            }

            var result = new StreamedResult(chunk);

            return result.AsDescription();
        }
    }
}
