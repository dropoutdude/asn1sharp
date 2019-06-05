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
    }
}
