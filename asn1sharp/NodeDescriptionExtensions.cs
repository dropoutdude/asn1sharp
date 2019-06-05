using System;
using System.Collections.Generic;
using System.Linq;

namespace asn1sharp
{
    internal static class NodeDescriptionExtensions
    {
        public static IEnumerable<NodeDescription> Inner(this NodeDescription description)
        {
            if (description.IsConstructed)
            {
                var offset = 0;

                while (TryParseNext(description.Data, offset, out var next))
                {
                    offset += (int)next.Header.NodeLength;

                    yield return next;
                }

                if (offset != description.Data.Length)
                {
                    throw new InvalidOperationException("Given node data was invalid!");
                }
            }
        }

        private static bool TryParseNext(byte[] data, int offset, out NodeDescription next)
        {
            next = null;

            if (data.Length > offset)
            {
                var nextNode = data.Skip(offset).ToArray();

                var header = NodeDescriptionHeader.From(nextNode);

                if (data.Length >= offset + header.NodeLength)
                {
                    next = new NodeDescription(
                                header,
                                data.Skip(offset + header.HeaderSize)
                                    .Take((int)header.DataLength).ToArray());
                }
            }

            return next != null;
        }
    }
}
