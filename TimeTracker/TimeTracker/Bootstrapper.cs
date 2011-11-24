using System;
using System.Data.Entity;
using StructureMap;
using TimeTracker.Domain;
using TimeTracker.DomainLayer;
using TimeTracker.Infrastructure;
using UserActivity;

namespace TimeTracker
{
	public class Bootstrapper : IBootstrapper
	{	
		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
		}
		
		public void BootstrapStructureMap()
		{
			ObjectFactory.Configure(x=>{
            	x.AddRegistry(new UserActivityRegistry());
				
				x.For<IRepository>()
					.Use<Repository>();
				
				x.For<DbContext>()
					.Use<Journal>();
			});
		}
	}
}
