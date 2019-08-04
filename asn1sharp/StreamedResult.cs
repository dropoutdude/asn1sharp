using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asn1sharp
{
    internal sealed class StreamedResult
    {
        #region Fields

        private readonly ReaderChunk _Chunk;

        #endregion

        #region Constructor

        public StreamedResult(ReaderChunk chunk)
        {
            _Chunk = chunk.RequireNotNull(nameof(chunk))
                              .Require(c => c.Length > 0);
        }

        #endregion

        #region Methods

        public NodeDescription AsDescription()
        {
            var length = _Chunk.Length;

            var data = new byte[length];

            if (!_Chunk.TryRead(data))
            {
                throw new InvalidOperationException("Chunk size not valid!");
            }

            return NodeDescription.From(data);
        }

        #endregion
    }
}
