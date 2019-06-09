using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
    public static class Parser
    {
        public static async Task<Node> Parse(this Stream stream)
        {
            var description = await Description(stream);

            description.Require(d => d.IsConstructed, "Invalid root node!");

            return Node.From(description);
        }

        private static async Task<NodeDescription> Description(Stream stream)
        {
            var read = -1;

            var buffers = new List<byte[]>();

            while (read != 0)
            {
                var buffer = new byte[1024];

                read = await stream.ReadAsync(buffer, 0, buffer.Length);

                buffers.Add(buffer);
            }

            var result = new StreamedResult(buffers);

            return result.AsDescription();
        }
    }
}
