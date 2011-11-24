using System;
using StructureMap;

namespace TimeTracker
{
	public static class Container
	{
		public static T Get<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}
	}
}
