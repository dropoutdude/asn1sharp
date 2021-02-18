using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace asn1sharp
{
    internal sealed class PemSource : ReaderSource, IDisposable
    {
        #region Fields

        private const string StartPattern = @"-----BEGIN (?<type>[\w\s]+)-----";

        private const string EndPattern = @"-----END (?<type>[\w\s]+)-----";

        private readonly StreamReader _Reader;

        #endregion

        #region Constructor

        public PemSource(Stream stream)
            : base(stream)
        {
            _Reader = new StreamReader(stream);

            Type = ReadType(_Reader).Require(s => s != string.Empty, "Invalid PEM structure!");
        }

        #endregion

        #region Properties

        public string Type { get; }

        #endregion

        #region Methods

        private static string ReadType(StreamReader reader)
        {
            var type = string.Empty;

            while (!reader.EndOfStream && type == string.Empty)
            {
                var line = reader.ReadLine();

                var match = Regex.Match(line, StartPattern);

                if (match.Success)
                {
                    type = match.Groups["type"].Value;
                }
            }

            return type;
        }

        #endregion

        #region Overridden from ReaderSource

        public override bool HasNextChunk()
        {
            return !_Reader.EndOfStream;
        }

        protected override async Task<ReaderChunk> OnNextChunk(CancellationToken token)
        {
            var chunk = ReaderChunk.Empty;

            if (!_Reader.EndOfStream)
            {
                token.ThrowIfCancellationRequested();

                var line = await _Reader.ReadLineAsync().ConfigureAwait(false);

                if (!(Regex.IsMatch(line, StartPattern) || Regex.IsMatch(line, EndPattern)))
                {
                    var bytes = Convert.FromBase64String(line);

                    chunk = ReaderChunk.From(bytes);
                }
            }

            return chunk;
        }

        #endregion

        #region Interface IDisposable

        public void Dispose()
        {
            _Reader.Dispose();
        }

        #endregion
    }
}
