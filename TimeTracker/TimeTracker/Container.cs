using System;
using StructureMap;

namespace TimeTracking
{
	public static class Container
	{
		public static T Get<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}
	}
}
