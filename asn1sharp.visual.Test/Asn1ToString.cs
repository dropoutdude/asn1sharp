using asn1sharp.Test;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace asn1sharp.visual.Test
{
    public class Asn1ToStringTest
    {
        [Fact]
        public async Task PrettyPrint_EccKey_ExpectSpecificLines()
        {
            var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

            var asn1node = await base64.Parse();

            var printed = asn1node.Print();
        }

        [Fact]
        public async Task PrettyPrint_RsaKey_ExpectSpecificLines()
        {
            var base64 = PemReader.ReadPem(Path.Combine("TestData", "rsakey1.pem"));

            var asn1node = await base64.Parse();

            var printed = asn1node.Print();
        }
    }
}
