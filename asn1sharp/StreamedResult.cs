using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asn1sharp
{
    internal sealed class StreamedResult
    {
        #region Fields

        private readonly List<byte[]> _Buffers;

        #endregion

        #region Constructor

        public StreamedResult(List<byte[]> buffers)
        {
            _Buffers = buffers.RequireNotNull(nameof(buffers))
                              .Require(b => b.Any());
        }

        #endregion

        #region Methods

        public NodeDescription AsDescription()
        {
            var length = _Buffers.Aggregate(0, (l, b) => l + b.Length);

            var data = new byte[length];

            var offset = 0;

            foreach (var buffer in _Buffers)
            {
                Array.Copy(buffer, 0, data, offset, buffer.Length);

                offset += buffer.Length;
            }

            return NodeDescription.From(data);
        }

        #endregion
    }
}
