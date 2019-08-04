using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
    public partial class ReaderChunk
    {
        private sealed class SingleChunk : ReaderChunk
        {
            #region Fields

            private readonly byte[] _Data;

            private int _CurrentIndex = 0;

            #endregion

            #region Constructor

            public SingleChunk()
            {
                _Data = new byte[0];
            }

            public SingleChunk(byte[] asn1data)
            {
                _Data = asn1data.RequireNotNull(nameof(asn1data)).Require(d => d.Length > 0);
            }

            #endregion

            #region Overridden from ReaderChunk

            protected override int AvailableBytes()
            {
                return _Data.Length - _CurrentIndex;
            }
            
            protected override int ReadInto(byte[] buffer, int offset)
            {
                var chunkSize = Math.Min(buffer.Length - offset, AvailableBytes());

                Array.Copy(_Data, _CurrentIndex, buffer, offset, chunkSize);

                _CurrentIndex += chunkSize;

                return chunkSize;
            }

            #endregion
        }
    }
}
