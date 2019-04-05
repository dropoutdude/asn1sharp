using System;
using System.Collections.Generic;
using System.Text;

namespace asn1sharp
{
	public static class Constraints
	{
		public static T Require<T>(this T value, Predicate<T> predicate)
		{
			if (!predicate(value))
			{
				throw new ArgumentException("Given parameter does not fulfill required conditions!");
			}

			return value;
		}
	}
}
