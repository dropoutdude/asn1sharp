using System.IO;
using Xunit;

namespace asn1sharp.Test
{
	public class Asn1ParserTest
	{
		[Fact]
		public void ParseRsaKeyPair_ExpectSpecificStructure()
		{
			using (var file = File.Open("TestData\\rsakey1.pem", FileMode.Open))
			{
				var base64 = PemReader.ReadPem(file);

				var node = base64.Parse();
			}
		}

		[Fact]
		public void ParseEccKeyPair_ExpectSpecificStructure()
		{
			using (var file = File.Open("TestData\\bp384-key1.pem", FileMode.Open))
			{
				var base64 = PemReader.ReadPem(file);

				var node = base64.Parse();
			}
		}
	}
}
