using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
	public sealed class NodeType : IEquatable<NodeType>
	{
		#region Fields

		private readonly byte _Value;

		public static readonly NodeType Integer = new NodeType(0x02);
		public static readonly NodeType BitString = new NodeType(0x03);
		public static readonly NodeType OctetString = new NodeType(0x04);
		public static readonly NodeType Null = new NodeType(0x05);
		public static readonly NodeType Oid = new NodeType(0x06);
		public static readonly NodeType Sequence = new NodeType(0x10);
		public static readonly NodeType Set = new NodeType(0x11);
		public static readonly NodeType PrintableString = new NodeType(0x13);

		#endregion

		#region Constructor

		private NodeType(byte value)
		{
			_Value = value.Require(v => v > 0);
		}

		public static NodeType From(byte value)
		{
			var type = value & 0b0001_1111;

			return new NodeType((byte)type);
		}

		#endregion

		#region Interface IEquatable

		public bool Equals(NodeType other)
		{
			return this == other;
		}

		public static bool operator ==(NodeType left, NodeType right)
		{
			if (ReferenceEquals(left, right))
			{
				return true;
			}

			if (left is null || right is null)
			{
				return false;
			}

			return left._Value == right._Value;
		}

		public static bool operator !=(NodeType left, NodeType right)
		{
			return !(left == right);
		}

		#endregion

		#region Overridden from Object

		public override int GetHashCode()
		{
			return _Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as NodeType);
		}

		#endregion
	}
}
