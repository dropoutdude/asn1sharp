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

			var outerDescription = NodeDescription.From(bytes);

			var offset = outerDescription.ValueOffset;

			var firstInnerDescription = NodeDescription.From(bytes.Skip(offset).ToArray());

			Assert.Equal(2, outerDescription.LengthData.Length);
			Assert.Equal(4, offset);

			Assert.Single(firstInnerDescription.LengthData);
			Assert.Equal(2, firstInnerDescription.ValueOffset);
		}

		[Fact]
		public void ParseEccDescription_ExpectSpecificStructure()
		{
			var base64 = PemReader.ReadPem(Path.Combine("TestData", "bp384-key1.pem"));

			var bytes = Convert.FromBase64String(base64);

			var outerDescription = NodeDescription.From(bytes);

			var offset = outerDescription.ValueOffset;

			var firstInnerDescription = NodeDescription.From(bytes.Skip(offset).ToArray());

			Assert.Single(outerDescription.LengthData);
			Assert.True(outerDescription.Length() > 127);
			Assert.Equal(3, offset);

			Assert.Single(firstInnerDescription.LengthData);
			Assert.Equal(2, firstInnerDescription.ValueOffset);
		}
	}
}
