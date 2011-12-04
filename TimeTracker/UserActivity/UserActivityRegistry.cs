
using System;
using StructureMap.Configuration.DSL;

namespace UserActivity
{
	public class UserActivityRegistry : Registry
	{
		public UserActivityRegistry()
		{
			For<IKernel32>().Use<Kernel32Api>();
			For<IUser32>().Use<User32Api>();
			For<IKeyboard>().Use<Keyboard>();
		}
	}
}
