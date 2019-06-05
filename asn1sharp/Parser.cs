using System;
using System.IO;
using System.Threading.Tasks;

namespace asn1sharp
{
	public static class Parser
	{
		public static async Task<Node> Parse(this Stream stream)
		{
			var description = await Description(stream);

            var children = description.Inner();

			throw new NotImplementedException();
		}

		private static async Task<NodeDescription> Description(Stream stream)
		{
			var buffer = new byte[1024];

			var read = await stream.ReadAsync(buffer, 0, buffer.Length);

			var first = NodeDescriptionHeader.From(buffer);

			var data = BuildData(buffer, (int)first.DataLength, first.HeaderSize, read - first.HeaderSize);

			var left = (int)first.NodeLength - read;

			if (left > 0)
			{
				await stream.ReadAsync(data, read, left);
			}

			return new NodeDescription(first, data);
		}

		private static byte[] BuildData(byte[] buffer, int length, int offset, int read)
		{
			var data = new byte[length];

			Array.Copy(buffer, offset, data, 0, read);

			return data;
		}
	}
}
