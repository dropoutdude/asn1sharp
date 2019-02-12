using System.IO;
using System.Text;

namespace asn1sharp.Test
{
	internal class PemReader
	{
		public static string ReadPem(Stream stream)
		{
			var builder = new StringBuilder();

			using (var reader = new StreamReader(stream))
			{
				var line = string.Empty;

				do
				{
					line = reader.ReadLine();

					if (line != null && !line.StartsWith("-----"))
					{
						builder.Append(line);
					}
				}
				while (!string.IsNullOrWhiteSpace(line));
			}

			return builder.ToString();
		}
	}
}
