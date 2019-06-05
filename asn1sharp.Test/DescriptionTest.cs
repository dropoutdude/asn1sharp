using System;
using System.IO;
using System.Linq;
using Xunit;

namespace asn1sharp.Test
{
	public class DescriptionTest
	{
		[Fact]
		public void ParseRsaDescription_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "rsakey1.pem"));

			var bytes = Convert.FromBase64String(base64);

			var outerDescription = NodeDescriptionHeader.From(bytes);

			var offset = outerDescription.HeaderSize;

			var firstInnerDescription = NodeDescriptionHeader.From(bytes.Skip(offset).ToArray());

			Assert.Equal(1213, outerDescription.DataLength);
			Assert.Equal(4, offset);

			Assert.Equal(1, firstInnerDescription.DataLength);
			Assert.Equal(2, firstInnerDescription.HeaderSize);
		}

		[Fact]
		public void ParseEccDescription_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

			var bytes = Convert.FromBase64String(base64);

			var outerDescription = NodeDescriptionHeader.From(bytes);

			var offset = outerDescription.HeaderSize;

			var firstInnerDescription = NodeDescriptionHeader.From(bytes.Skip(offset).ToArray());

			Assert.Equal(168, outerDescription.DataLength);
			Assert.True(outerDescription.DataLength > 127);
			Assert.Equal(3, offset);

			Assert.Equal(1, firstInnerDescription.DataLength);
			Assert.Equal(2, firstInnerDescription.HeaderSize);
		}
	}
}
