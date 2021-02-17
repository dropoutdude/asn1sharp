using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace asn1sharp.Test
{
    public class Asn1ParserTest
    {
        [Fact]
        public async Task ParsePemRsaKeyPair_ExpectSpecificStructure()
        {
            using (var file = File.OpenRead(Path.Combine("TestData", "rsakey1.pem")))
            {
                var node = await file.ParsePem();

                AssertNode(node);
            }
        }

        [Fact]
        public async Task ParseBinaryRsaKeyPair_ExpectSpecificStructure()
        {
            using (var file = File.OpenRead(Path.Combine("TestData", "rsakey1.pem")))
            {
                var bytes = Convert.FromBase64String(PemReader.ReadPem(file));

                var node = await bytes.ParseBinary();

                AssertNode(node);
            }
        }

        private void AssertNode(Node node)
        {
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
            using (var file = File.OpenRead(Path.Combine("TestData", "bp384-key1.pem")))
            {
                var node = await file.ParsePem();
            }
        }
    }
}
