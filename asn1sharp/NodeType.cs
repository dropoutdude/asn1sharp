using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
    public sealed class NodeType : IEquatable<NodeType>
    {
        #region Fields

        private readonly byte _Value;

        private readonly string _Name;

        public static readonly NodeType Integer = new NodeType(0x02, "Integer");
        public static readonly NodeType BitString = new NodeType(0x03, "Bit String");
        public static readonly NodeType OctetString = new NodeType(0x04, "Octet String");
        public static readonly NodeType Null = new NodeType(0x05, "NULL");
        public static readonly NodeType Oid = new NodeType(0x06, "OID");
        public static readonly NodeType Sequence = new NodeType(0x10, "Sequence");
        public static readonly NodeType Set = new NodeType(0x11, "Set");
        public static readonly NodeType PrintableString = new NodeType(0x13, "Printable String");

        #endregion

        #region Constructor

        private NodeType(byte value, string name)
        {
            _Value = value.Require(v => v >= 0, "Type value must be a positive number!");

            _Name = name.RequireNotEmpty(nameof(name));
        }

        public static NodeType Specific(byte value)
        {
            var type = value & 0b0001_1111;

            return new NodeType((byte)type, "Context specific");
        }

        public static NodeType Universal(byte value)
        {
            var type = value & 0b0001_1111;

            switch (type)
            {
                case 0x02:
                    return Integer;
                case 0x03:
                    return BitString;
                case 0x04:
                    return OctetString;
                case 0x05:
                    return Null;
                case 0x06:
                    return Oid;
                case 0x10:
                    return Sequence;
                case 0x11:
                    return Set;
                case 0x13:
                    return PrintableString;
                default:
                    return new NodeType((byte)type, "Application specific");
            }
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

            return left._Value == right._Value 
                && left._Name == right._Name;
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

        public override string ToString()
        {
            return _Name;
        }
        #endregion
    }
}
