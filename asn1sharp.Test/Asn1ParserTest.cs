using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace asn1sharp.Test
{
	public class Asn1ParserTest
	{
		[Fact]
		public async Task ParseRsaKeyPair_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "rsakey1.pem"));

			var node = await base64.Parse();
		}

		[Fact]
		public async Task ParseEccKeyPair_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

			var node = await base64.Parse();
		}
	}
}
