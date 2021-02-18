using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace asn1sharp
{
    internal sealed class BinarySource : ReaderSource
    {
        #region Fields

        private int _ReadBytes = -1;

        #endregion

        #region Constructor

        public BinarySource(Stream stream)
            : base(stream)
        {

        }

        #endregion

        #region Overridden from ReaderSource

        public override bool HasNextChunk()
        {
            return _ReadBytes != 0;
        }

        protected override async Task<ReaderChunk> OnNextChunk(CancellationToken token)
        {
            var chunk = ReaderChunk.Empty;

            if (_ReadBytes != 0)
            {
                var buffer = new byte[ChunkSize];

                _ReadBytes = await Stream.ReadAsync(buffer, 0, buffer.Length, token)
                                         .ConfigureAwait(false);

                if (_ReadBytes > 0)
                {
                    Array.Resize(ref buffer, _ReadBytes);

                    chunk = ReaderChunk.From(buffer);
                }
            }

            return chunk;
        }

        #endregion
    }
}
