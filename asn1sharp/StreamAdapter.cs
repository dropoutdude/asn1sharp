using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace asn1sharp
{
    internal sealed class StreamAdapter : Stream
    {
        #region Fields

        private readonly FileStream _Inner;

        private readonly StreamReader _Reader;

        private byte[] _Buffer = Array.Empty<byte>();

        #endregion

        #region Constructor

        public StreamAdapter(FileStream stream)
        {
            _Inner = stream.RequireNotNull(nameof(stream));

            _Reader = new StreamReader(_Inner);
        }

        #endregion

        #region Methods

        private (byte[], int) ReadNextBytes(int count)
        {
            var result = new byte[count];
            var read = 0;

            while (read < count && !_Reader.EndOfStream)
            {
                var line = _Reader.ReadLine();

                if (!(line.StartsWith("-----BEGIN ") || line.StartsWith("-----END ")))
                {
                    var bytes = Convert.FromBase64String(line);

                    if (_Buffer.Length > 0)
                    {
                        bytes = _Buffer.Concat(bytes).ToArray();

                        _Buffer = Array.Empty<byte>();
                    }

                    var rest = Math.Min(count - read, bytes.Length);
                    
                    Array.Copy(bytes, 0, result, read, rest);

                    read += rest;

                    if (rest < bytes.Length)
                    {
                        var overflow = bytes.Length - rest;

                        _Buffer = new byte[overflow];

                        Array.Copy(bytes, rest, _Buffer, 0, overflow);
                    }
                }
            }

            return (result, read);
        }

        #endregion

        #region Overridden from Stream

        public override bool CanRead => _Inner.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _Inner.Length;

        public override long Position
        {
            get => _Inner.Position;
            set => _Inner.Position = value;
        }

        public override void Flush()
        {
            _Inner.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length) throw new ArgumentException("Buffer too small!");
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), "Positive value forexpected");
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Positive value forexpected");

            var (result, read) = ReadNextBytes(count);

            Array.Copy(result, 0, buffer, offset, read);

            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Cannot seek on this stream");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Cannot set the length");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Cannot write to the stream");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Reader.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
