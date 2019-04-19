using System;

namespace asn1sharp
{
	internal static class Constraints
	{
		public static T RequireNotNull<T>(this T value, string name)
			where T : class
		{
			if (value is null)
			{
				throw new ArgumentNullException(name);
			}

			return value;
		}

		public static T Require<T>(this T value, Predicate<T> predicate, string message = "")
		{
			if (!predicate(value))
			{
				var exceptionMessage = "Given parameter does not fulfill required conditions!" +
										$"{Environment.NewLine}\tCause: {message}";

				throw new ArgumentException(exceptionMessage);
			}

			return value;
		}
	}
}
