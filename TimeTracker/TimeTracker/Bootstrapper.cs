using System;
using StructureMap;
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

				x.For<ITimesheet>()
					.Use<Timesheet>()
					.Ctor<string>("fileName")
					.EqualToAppSetting("Timesheet");
				
				x.For<IFile>()
					.Use<FileAdapter>()
					.Ctor<string>("fileName")
					.EqualToAppSetting("RecentActivitiesFile");
				
				x.For<IRecentActivities>()
					.Use<RecentActivities>();
			});
		}
	}
}
