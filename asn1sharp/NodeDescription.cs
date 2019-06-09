using System;

namespace asn1sharp
{
    internal sealed class NodeDescription
    {
        #region Constructor

        public NodeDescription(NodeDescriptionHeader header, byte[] data)
        {
            Header = header.RequireNotNull(nameof(header));

            Data = data.RequireNotNull(nameof(data))
                       .Require(d => d.Length == header.DataLength);
        }

        #endregion

        #region Properties

        public NodeDescriptionHeader Header { get; }

        public byte[] Data { get; }

        public bool IsConstructed => Header.IsConstructed();

        #endregion

        #region Methods

        public static NodeDescription From(byte[] data)
        {
            var header = NodeDescriptionHeader.From(data);

            if (data.Length >= header.NodeLength)
            {
                var nodeData = new byte[header.DataLength];

                Array.Copy(data, header.HeaderSize, nodeData, 0, nodeData.Length);

                return new NodeDescription(header, nodeData);
            }
            else
            {
                throw new InvalidOperationException("Invalid data given!");
            }
        }

        #endregion
    }
}
