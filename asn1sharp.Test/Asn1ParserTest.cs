using System.IO;
using System.Linq;
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

            var children = node.Children;

            var grandChildren = children.SelectMany(c => c.Children);

            var key = grandChildren.Last();

            Assert.Equal(3, children.Count());

            Assert.Equal(3, grandChildren.Count());

            Assert.Equal(9, key.Children.Count());
        }

        [Fact]
        public async Task ParseEccKeyPair_ExpectSpecificStructure()
        {
            var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

            var node = await base64.Parse();
        }
    }
}
