using System;
using System.Linq;

namespace asn1sharp
{
	internal sealed class NodeDescription
	{
		#region Constructor

		private NodeDescription(byte[] tag, byte[] length, NodeDescription inner)
		{

		}

		#endregion

		#region Properties

		public byte[] Tag { get; }

		public byte[] Length { get; }

		#endregion

		#region Methods

		public uint Lenght()
		{
			return BitConverter.ToUInt32(Length.Take(4).ToArray(), 0);
		}

		public static NodeDescription From(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
