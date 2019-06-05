using System;
using System.Linq;

namespace asn1sharp
{
	internal sealed class NodeDescriptionHeader
	{
		#region Fields

		private const int MinimumLength = 3;

		private const int MinimumHeaderSize = 2;

		#endregion

		#region Constructor

		private NodeDescriptionHeader(byte tag, long length, int headerSize)
		{
			Tag = tag;

			DataLength = length.Require(l => l >= 0, "Data length value must be positive!");

			HeaderSize = headerSize.Require(o => o >= MinimumHeaderSize, $"{nameof(headerSize)} must be greater than {MinimumHeaderSize}!");
		}

		#endregion

		#region Properties

		public byte Tag { get; }

		public int HeaderSize { get; }

		public long DataLength { get; }

        public long NodeLength => DataLength + HeaderSize;

		#endregion

		#region Methods

		public bool IsConstructed()
		{
			return Tag.IsConstructed();
		}

		private static long LengthValue(byte[] lengthData)
		{
			if (lengthData.Length <= 4)
			{
				var intValue = lengthData.Reverse().Concat(Enumerable.Repeat(byte.MinValue, sizeof(long) - lengthData.Length)).ToArray();

				return BitConverter.ToInt64(intValue, 0);
			}

			throw new InvalidOperationException("Can only handle Data of size less than 2^63 bytes");
		}

		public static NodeDescriptionHeader From(byte[] bytes)
		{
			bytes.Require(b => b.Length > MinimumLength, "Too little data provided for reading ASN.1!");

			var tag = ReadTag(bytes);

			var (lengthData, lengthSize) = ReadLength(bytes.Skip(1).ToArray());

			var length = LengthValue(lengthData);
            
            // Increase lengthSize by one to account for the tag byte
			return new NodeDescriptionHeader(tag, length, lengthSize + 1);
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
				offset += length;

				return (bytes.Skip(1).Take(length).ToArray(), offset);
			}

			return (bytes.Take(1).ToArray(), offset);
		}

		#endregion
	}
}
