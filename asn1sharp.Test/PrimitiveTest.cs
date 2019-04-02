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
	}
}
