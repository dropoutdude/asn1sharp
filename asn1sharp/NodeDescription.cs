using System;
using System.Linq;

namespace asn1sharp
{
	internal sealed class NodeDescription
	{
		#region Fields

		private const int MinimumLength = 3;

		private const int MinimumOffset = 2;

		#endregion

		#region Constructor

		private NodeDescription(byte tag, byte[] lengthData, int valueOffset)
		{
			Tag = tag;

			LengthData = lengthData.RequireNotNull(nameof(lengthData));

			ValueOffset = valueOffset.Require(o => o >= MinimumOffset, $"{nameof(valueOffset)} must be greater than {MinimumOffset}!");
		}

		#endregion

		#region Properties

		public byte Tag { get; }

		public byte[] LengthData { get; }

		public int ValueOffset { get; }

		#endregion

		#region Methods

		public int Length()
		{
			if (LengthData.Length == 1)
			{
				return LengthData[0];
			}

			return BitConverter.ToInt32(LengthData.Take(4).ToArray(), 0);
		}

		public static NodeDescription From(byte[] bytes)
		{
			bytes.Require(b => b.Length > MinimumLength, "Too little data provided for reading ASN.1!");

			var tag = ReadTag(bytes);

			var (length, offset) = ReadLength(bytes.Skip(1).ToArray());

			return new NodeDescription(tag, length, offset + 1);
		}

		private static byte ReadTag(byte[] bytes)
		{
			return bytes[0];
		}

		private static (byte[], int) ReadLength(byte[] bytes)
		{
			bytes.Require(b => b.Length > 0, "Too little data provided for reading ASN.1!");

			var offset = 1;

			var isLong = (bytes[0] >> 7 & 0x01) == 1;

			var length = (bytes[0] & 0x7F);

			if (isLong && length > 0)
			{
				offset = offset + length;

				return (bytes.Skip(1).Take(length).ToArray(), offset);
			}

			return (bytes.Take(1).ToArray(), offset);
		}

		#endregion
	}
}
