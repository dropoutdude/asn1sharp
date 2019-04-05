namespace asn1sharp
{
	public sealed class Tag
	{
		#region Constructor

		public Tag(byte value)
		{
			Class = value.Class();

			Type = value.Type();

			IsConstructed = value.IsConstructed();
		}

		#endregion

		#region Properties

		public NodeClass Class { get; }

		public NodeType Type { get; }

		public bool IsConstructed { get; }

		#endregion
	}
}
