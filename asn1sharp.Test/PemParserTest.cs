using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace asn1sharp.Test
{
    public class PemParserTest
    {
        [Fact]
        public async Task ReadRsaPemFile_CompareResult_ExpectSameData()
        {
            var path = Path.Combine("TestData", "rsakey1.pem");

            using (var file = File.OpenRead(path))
            {
                var actualNode = await file.ParsePem();

                var bytesExpected = Convert.FromBase64String(PemReader.ReadPem(path));
                var expectedNode = await bytesExpected.ParseBinary();

                var actual = ReadNode(actualNode);
                var expected = ReadNode(expectedNode);

                Assert.Equal(expected.children.Count(), actual.children.Count());
                Assert.Equal(expected.grandChildren.Count(), actual.grandChildren.Count());
                Assert.Equal(expected.key.Children.Count(), actual.key.Children.Count());
            }
        }

        private (IEnumerable<Node> children, IEnumerable<Node> grandChildren, Node key) ReadNode(Node parent)
        {
            var children = parent.Children;

            var grandChildren = children.SelectMany(c => c.Children);

            var key = grandChildren.Last();

            return (children, grandChildren, key);
        }
    }
}
