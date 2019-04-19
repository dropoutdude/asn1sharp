using System.IO;
using Xunit;

namespace asn1sharp.Test
{
	public class Asn1ParserTest
	{
		[Fact]
		public void ParseRsaKeyPair_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "rsakey1.pem"));

			var node = base64.Parse();
		}

		[Fact]
		public void ParseEccKeyPair_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

			var node = base64.Parse();
		}
	}
}
