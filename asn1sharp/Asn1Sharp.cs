using System;

namespace asn1sharp
{
	public static class Asn1Sharp
	{
		public static Node Parse(this byte[] bytes)
		{
			throw new NotImplementedException();
		}

		public static Node Parse(this string base64Values)
		{
			var bytes = Convert.FromBase64String(base64Values);

			return bytes.Parse();
		}
	}
}
