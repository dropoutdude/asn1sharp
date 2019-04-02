namespace asn1sharp
{
	public enum NodeClass
	{
		Universal,
		Application,
		ContextSpecific,
		Private
	}

	internal static class NodeClassExtensions
	{
		public static NodeClass Class(this byte source)
		{
			var value = (source >> 6) & 0x03;

			return (NodeClass)value;
		}
	}
}
