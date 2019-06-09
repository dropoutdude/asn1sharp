using System;
using System.Collections.Generic;
using System.Linq;

namespace asn1sharp
{
    internal static class NodeDescriptionExtensions
    {
        public static IEnumerable<NodeDescription> Children(this NodeDescription parent)
        {
            var (length, nodes) = Children(parent.Data);

            if (length != parent.Data.Length)
            {
                if (parent.IsConstructed)
                {
                    throw new InvalidOperationException("Given node data was invalid!");
                }
                else
                {
                    return Enumerable.Empty<NodeDescription>();
                }
            }

            return nodes;
        }

        private static (int totalLength, IEnumerable<NodeDescription>) Children(byte[] data)
        {
            var length = 0;

            List<NodeDescription> nodes = new List<NodeDescription>();

            while (TryParseNext(data, length, out var next))
            {
                length += (int)next.Header.NodeLength;

                nodes.Add(next);
            }

            return (length, nodes);
        }

        private static bool TryParseNext(byte[] data, int offset, out NodeDescription next)
        {
            next = null;

            if (data.Length > offset)
            {
                var nextNode = data.Skip(offset).ToArray();

                if (NodeDescriptionHeader.TryCreateFrom(nextNode, out var header) &&
                    data.Length >= offset + header.NodeLength)
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
