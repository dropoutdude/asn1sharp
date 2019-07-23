using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
    public sealed class Sizes
    {
        #region Constructor

        private Sizes(int header, long data)
        {
            Header = header.Require(s => s > 0, "Header size must be a positive value!");

            Data = data.Require(s => s >= 0, "Data size must be a positive value!");
        }

        #endregion

        #region Properties

        public long Total => Data + Header;

        public long Data { get; }

        public int Header { get; }

        #endregion

        internal static Sizes From(NodeDescription description)
        {
            return new Sizes(description.Header.HeaderSize, description.Header.DataLength);
        }
    }
}
