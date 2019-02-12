using System.Collections.Generic;

namespace asn1sharp
{
	public abstract class Node
	{
		#region Properties

		#endregion

		#region Abstract Interface

		protected abstract IEnumerable<Node> ParseChildren();

		#endregion
	}
}
