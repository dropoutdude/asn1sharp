using System;
using System.Linq;

namespace asn1sharp
{
    internal sealed class NodeDescriptionHeader
    {
        #region Fields

        private const int MinimumLength = 2;

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

        public static NodeDescriptionHeader From(byte[] bytes)
        {
            bytes.Require(b => b.Length >= MinimumLength, "Too little data provided for reading ASN.1!");

            if(!TryCreateFrom(bytes, out var header, out var message))
            {
                throw new ArgumentException(message);
            }

            return header;
        }

        public static bool TryCreateFrom(byte[] bytes, out NodeDescriptionHeader header)
        {
            header = null;

            if (bytes.Length >= MinimumLength)
            {
                TryCreateFrom(bytes, out header, out _);
            }

            return header != null;
        }

        private static bool TryCreateFrom(byte[] bytes, out NodeDescriptionHeader header, out string message)
        {
            message = string.Empty;

            var tag = ReadTag(bytes);

            var (lengthData, lengthSize) = ReadLength(bytes.Skip(1).ToArray());

            if (lengthData.Length > sizeof(long))
            {
                header = null;

                message = "Can only handle Data of size less than 2^63 bytes";
            }
            else
            {
                var length = LengthValue(lengthData);

                // Increase lengthSize by one to account for the tag byte
                var headerLenght = lengthSize + 1;

                header = new NodeDescriptionHeader(tag, length, headerLenght);
            }

            return header != null;
        }

        private static long LengthValue(byte[] lengthData)
        {
            var longValue = lengthData.Reverse().Concat(Enumerable.Repeat(byte.MinValue, sizeof(long) - lengthData.Length)).ToArray();

            return BitConverter.ToInt64(longValue, 0);
        }

        private static byte ReadTag(byte[] bytes)
        {
            return bytes[0];
        }

        private static (byte[], int) ReadLength(byte[] bytes)
        {
            if (bytes.Length > 0)
            {
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
            else
            {
                return (bytes, 1);
            }
        }

        #endregion
    }
}
