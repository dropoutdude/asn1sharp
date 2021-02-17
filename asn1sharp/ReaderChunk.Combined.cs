using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
    public partial class ReaderChunk
    {
        private sealed class CombinedReaderChunk : ReaderChunk
        {
            #region Fields

            private readonly ReaderChunk _First;

            private readonly ReaderChunk _Second;

            #endregion

            #region Constructor

            public CombinedReaderChunk(ReaderChunk first, ReaderChunk second)
            {
                _First = first.RequireNotNull(nameof(first));

                _Second = second.RequireNotNull(nameof(second));
            }

            #endregion

            protected override int AvailableBytes()
            {
                return _First.AvailableBytes() + _Second.AvailableBytes();
            }

            protected override int ReadInto(byte[] buffer, int offset)
            {
                var fromFirst = _First.ReadInto(buffer, offset);

                var fromSecond = _Second.ReadInto(buffer, offset + fromFirst);

                return fromFirst + fromSecond;
            }
        }
    }
}
