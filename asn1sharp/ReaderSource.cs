using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace asn1sharp
{
    public abstract class ReaderSource
    {
        #region Constants

        protected const int ChunkSize = 1024;

        #endregion

        #region Constructor

        protected ReaderSource(Stream stream)
        {
            Stream = stream
                        .RequireNotNull(nameof(stream))
                        .Require(s => s.CanRead, "Stream cannot be read from - invalid as source!");
        }

        #endregion

        #region Properties

        protected Stream Stream { get; }

        #endregion

        #region Methods

        public Task<ReaderChunk> NextChunk(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return OnNextChunk(token);
        }

        protected abstract Task<ReaderChunk> OnNextChunk(CancellationToken token);

        public abstract bool HasNextChunk();

        #endregion
    }
}
