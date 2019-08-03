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
            var asn1node = await ReadPemFile("bp384-key1.pem");

            var printed = asn1node.Print();
        }

        [Fact]
        public async Task PrettyPrint_RsaKey_ExpectSpecificLines()
        {
            var asn1node = await ReadPemFile("rsakey1.pem");

            var printed = asn1node.Print();
        }

        private async Task<Node> ReadPemFile(string fileName)
        {
            using (var file = File.OpenRead(Path.Combine("TestData", fileName)))
            {
                return await file.Parse();
            }
        }
    }
}
