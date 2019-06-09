using Xunit;

namespace asn1sharp.Test
{
	public class PrimitiveTest
	{
		[Fact]
		public void ParseValidTagClassValue_ExpectValidClass()
		{
			var universal = new Tag(0b0001_1011);

			var application = new Tag(0b0101_1011);

			var context = new Tag(0b1001_1011);

			var privateClass = new Tag(0b1101_1011);

			Assert.Equal(NodeClass.Universal, universal.Class);
			Assert.Equal(NodeClass.Application, application.Class);
			Assert.Equal(NodeClass.ContextSpecific, context.Class);
			Assert.Equal(NodeClass.Private, privateClass.Class);
		}

		[Fact]
		public void ParseNodeType_ExpectValidEquality()
		{
			var integer = NodeType.Universal(0b0000_0010);
			var octetString = NodeType.Universal(0b0000_0100);
			var sequence = NodeType.Universal(0b0001_0000);
			var set = NodeType.Universal(0b0001_0001);

			Assert.Equal(NodeType.Integer, integer);
			Assert.NotEqual(NodeType.PrintableString, octetString);
			Assert.Equal(NodeType.OctetString, octetString);
			Assert.Equal(NodeType.Sequence, sequence);
			Assert.Equal(NodeType.Set, set);
		}

		[Fact]
		public void ParseValidTag_ExpectConstructedNode()
		{
			var t = new Tag(0b0011_0000);

			Assert.Equal(NodeType.Sequence, t.Type);
			Assert.Equal(NodeClass.Universal, t.Class);
			Assert.True(t.IsConstructed);
		}
	}
}
