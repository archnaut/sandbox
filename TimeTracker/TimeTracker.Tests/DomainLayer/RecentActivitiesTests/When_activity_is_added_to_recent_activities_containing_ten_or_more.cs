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
	public class When_activity_is_added_to_recent_activities_containing_ten_or_more
	{
	    private readonly string[] _activities = new[]
	                                                {
	                                                    "First Activity",
	                                                    "Second Activity",
	                                                    "Third Activity",
	                                                    "Forth Activity",
	                                                    "Fifth Activity",
	                                                    "Sixth Activity",
	                                                    "Seventh Activity",
	                                                    "Eighth Activity",
	                                                    "Ninth Activity",
	                                                    "Tenth Activity"
	                                                };
	
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
	
	        foreach(string activity in _activities)
	            _recentActivities.Add(activity);
	    }
	
	    [Test]
	    public void Last_activity_added_should_be_first()
	    {
	        Assert.AreEqual(_activities[9], _recentActivities.First);
	    }
	
	    [Test]
	    public void First_activity_added_should_be_last()
	    {
	    	Assert.AreEqual(_activities[0], _recentActivities.ToArray().Last());
	    }
	
	    private bool ContainsActivities(string[] content)
	    {
	        if (content.Length < 10)
	            return false;
	
	        return content.Length == 10 && content[0].Contains(_activities[10]) && content[9].Contains(_activities[1]);
	    }
	}
}
