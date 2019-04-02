using System;
using System.IO;

namespace asn1sharp
{
	public static class Parser
	{
		public static Node Parse(this Stream stream)
		{
			using (var reader = new StreamReader(stream))
			{
				throw new NotImplementedException();
			}
		}
	}
}
