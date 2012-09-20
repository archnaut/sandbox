using System;
using System.Data.Entity;
using StructureMap;
using StructureMap.Configuration.DSL;
using TimeTracking;
using TimeTracking.DomainLayer;
using TimeTracking.Infrastructure;
using UserActivity;

namespace TimeTracking
{
	public class TimeTrackingRegistry : Registry
	{	
		public TimeTrackingRegistry()
		{
			IncludeRegistry(new UserActivityRegistry());
	
			For<IRepository>()
				.Use<Repository>();
			
			For<DbContext>()
				.Use<Journal>();
			
			For<TimeTracker>()
				.Use<TimeTracker>();
										
			For<IRecentActivities>()
				.Use<RecentActivities>();
		}
	}
}
