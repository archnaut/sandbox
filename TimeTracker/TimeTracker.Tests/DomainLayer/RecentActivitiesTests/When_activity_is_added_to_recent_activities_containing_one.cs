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
	public class When_activity_is_added_to_recent_activities_containing_one
	{
	    private const string FIRST_ACTIVITY_NAME = "First Activity";
	    private const string SECOND_ACTIVITY_NAME = "Second Activity";

	    private RhinoAutoMocker<RecentActivities> _contianer;
	    private RecentActivities _recentActivities;
	
	    [SetUp]
	    public void SetUp()
	    {
	    	_contianer = new RhinoAutoMocker<RecentActivities>();
	        _recentActivities = _contianer.ClassUnderTest;
	        
	        _contianer.Get<IRepository>()
	        	.Stub(x=>x.AllInstances<Entry>())
	        	.Return(new List<Entry>().AsQueryable());
	
	        _recentActivities.Add(FIRST_ACTIVITY_NAME);
	        _recentActivities.Add(SECOND_ACTIVITY_NAME);
	    }
	
	    [Test]
	    public void activity_added_should_be_first_activity()
	    {
	        Assert.AreEqual(SECOND_ACTIVITY_NAME, _recentActivities.First);
	    }
	
	    [Test]
	    public void first_activity_added_should_be_last_activity()
	    {
	    	Assert.AreEqual(FIRST_ACTIVITY_NAME, _recentActivities.ToArray().Last());
	    }
	
	    private static bool ContainsActivities(string[] lines)
	    {
	        bool contains = true;
	        contains &= lines[0].Contains(FIRST_ACTIVITY_NAME);
	
	        if(lines.Length > 1)
	            contains &= lines[1].Contains(SECOND_ACTIVITY_NAME);
	        
	        return contains;
	    }
	   
	}
}
