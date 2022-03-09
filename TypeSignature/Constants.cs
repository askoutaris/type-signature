using System;
using System.Collections.Generic;

namespace TypeSignature
{
	public static class Constants
	{
		public static class SimpleTypes
		{
			public static readonly IReadOnlyCollection<Type> Defaults = new[] {
				typeof(string),
				typeof(decimal),
				typeof(int),
				typeof(long),
				typeof(TimeZoneInfo),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			};
		}
	}
}
