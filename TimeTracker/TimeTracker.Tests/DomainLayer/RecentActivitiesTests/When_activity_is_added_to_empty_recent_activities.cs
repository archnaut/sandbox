// ReSharper disable InconsistentNaming
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using TimeTracking.DomainLayer;
using System.Collections.Generic;

namespace TimeTracking.Tests.DomainLayer.RecentActivitiesTests
{
	[TestFixture]
	public class When_activity_is_added_to_empty_recent_activities
	{
	    private const string FIRST_ACTIVITY = "First Activity";
	    private RhinoAutoMocker<RecentActivities> _container;
	    private RecentActivities _recentActivities;
	
	    [SetUp]
	    public void SetUp()
	    {
	    	_container = new RhinoAutoMocker<RecentActivities>();
	        _recentActivities = _container.ClassUnderTest;
	        
	        _container.Get<IRepository>()
	        	.Stub(x=>x.AllInstances<Entry>())
	        	.Return(new List<Entry>().AsQueryable());
	
	        _recentActivities.Add(FIRST_ACTIVITY);
	    }
	
	    [Test]
	    public void activity_added_should_be_first_activity()
	    {
	        Assert.AreEqual(FIRST_ACTIVITY, _recentActivities.First);
	    }
	
	    [Test]
	    public void activity_added_should_be_last_activity()
	    {
	    	Assert.AreEqual(FIRST_ACTIVITY, _recentActivities.ToArray().Last());
	    }
	}
}
