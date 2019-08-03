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
            using (var file = File.OpenRead(Path.Combine("TestData", "rsakey1.pem")))
            {
                var node = await file.Parse();

                var children = node.Children;

                var grandChildren = children.SelectMany(c => c.Children);

                var key = grandChildren.Last();

                Assert.Equal(3, children.Count());

                Assert.Equal(3, grandChildren.Count());

                Assert.Equal(9, key.Children.Count());
            }
        }

        [Fact]
        public async Task ParseEccKeyPair_ExpectSpecificStructure()
        {
            using (var file = File.OpenRead(Path.Combine("TestData", "bp384-key1.pem")))
            {
                var node = await file.Parse();
            }
        }
    }
}
