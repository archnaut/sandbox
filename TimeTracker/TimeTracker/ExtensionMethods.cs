using System;
using System.Collections.Generic;

namespace TimeTracker
{
	public static class ExtensionMethods
	{
		public static Queue<T> ToQueue<T>(this IEnumerable<T> instance)
		{
			return new Queue<T>(instance);
		}
	}
}
