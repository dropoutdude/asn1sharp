namespace asn1sharp
{
	public sealed class Tag
	{
		#region Constructor

		public Tag(byte value)
		{
			Class = value.Class();
		}

		#endregion

		#region Properties

		public NodeClass Class { get; }

		#endregion
	}
}
