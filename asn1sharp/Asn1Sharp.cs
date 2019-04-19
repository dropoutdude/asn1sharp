using System;
using System.IO;

namespace asn1sharp
{
	public static class Asn1Sharp
	{
		public static Node Parse(this byte[] bytes)
		{
			using (var stream = new MemoryStream(bytes))
			{
				return stream.Parse();
			}
		}

		public static Node Parse(this string base64Values)
		{
			var bytes = Convert.FromBase64String(base64Values);

			return bytes.Parse();
		}
	}
}
