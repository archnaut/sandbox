using System;
using System.Collections.Generic;

namespace TimeTracking.Infrastructure
{
	public static class ExtensionMethods
	{
		public static Queue<T> ToQueue<T>(this IEnumerable<T> instance)
		{
			return new Queue<T>(instance);
		}
		
		public static string Compose(this string format, object arg)
		{
			return string.Format(format, arg);
		}
		
		public static string Compose(this string format, object arg0, object arg1)
		{
			return string.Format(format, arg0, arg1);
		}
		
		public static string Compose(this string format, object arg0, object arg1, object arg2)
		{
			return string.Format(format, arg0, arg1, arg2);
		}
		
		public static string Compose(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

	}
}
