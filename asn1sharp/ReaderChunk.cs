namespace asn1sharp
{
    public abstract partial class ReaderChunk
    {
        #region Factory

        public static ReaderChunk From(byte[] asn1Data)
        {
            return new SingleChunk(asn1Data);
        }

        #endregion

        #region Properties

        public static readonly ReaderChunk Empty = new SingleChunk();

        public int Length => AvailableBytes();

        #endregion

        #region Methods

        public ReaderChunk Combine(ReaderChunk other)
        {
            other.RequireNotNull(nameof(other));

            return new CombinedReaderChunk(this, other);
        }

        protected abstract int AvailableBytes();

        protected abstract int ReadInto(byte[] buffer, int offset);

        public bool TryRead(byte[] buffer)
        {
            var read = 0;

            if (AvailableBytes() >= buffer.Length)
            {
                read = ReadInto(buffer, offset: 0);
            }

            return read > 0;
        }

        #endregion
    }
}
