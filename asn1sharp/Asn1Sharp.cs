using System;
using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
	public static class Asn1Sharp
	{
		public static async Task<Node> Parse(this byte[] bytes)
		{
			using (var stream = new MemoryStream(bytes))
			{
				return await stream.Parse();
			}
		}

		public static async Task<Node> Parse(this string base64Values)
		{
			var bytes = Convert.FromBase64String(base64Values);

			return await bytes.Parse();
		}
	}
}
