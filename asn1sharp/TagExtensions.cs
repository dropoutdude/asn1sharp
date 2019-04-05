using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
	internal static class TagExtensions
	{
		public static NodeClass Class(this byte tag)
		{
			var value = (tag >> 6) & 0x03;

			return (NodeClass)value;
		}

		public static NodeType Type(this byte tag)
		{
			return NodeType.From(tag);
		}

		public static bool IsConstructed(this byte tag)
		{
			var value = (tag >> 5) & 0x01;

			return value == 1;
		}
	}
}
