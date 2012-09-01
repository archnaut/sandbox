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
    public class When_existing_activity_is_added_to_recent_activities
    {
        private const string EXISTING_ACTIVITY_NAME = "Existing Activity";
        private const string SECOND_ACTIVITY_NAME = "Second Activity";
        private const string THIRD_ACTIVITY_NAME = "Third Activity";

		private RhinoAutoMocker<RecentActivities> _container;
		private RecentActivities _systemUnderTest;

        [SetUp]
        public void SetUp()
        {
        	_container = new RhinoAutoMocker<RecentActivities>();
            _systemUnderTest = _container.ClassUnderTest;

            _container.Get<IRepository>()
            	.Stub(x=>x.AllInstances<Entry>())
            	.Return(new List<Entry>().AsQueryable());
            
            _systemUnderTest.Add(EXISTING_ACTIVITY_NAME);
            _systemUnderTest.Add(SECOND_ACTIVITY_NAME);
            _systemUnderTest.Add(THIRD_ACTIVITY_NAME);
        }

        [Test]
        public void existing_activity_should_become_first()
        {
            Assert.AreEqual(THIRD_ACTIVITY_NAME, _systemUnderTest.First);

            _systemUnderTest.Add(EXISTING_ACTIVITY_NAME);

            Assert.AreEqual(EXISTING_ACTIVITY_NAME, _systemUnderTest.First);
        }

        [Test]
        public void should_contain_stingle_instance_of_existing_activity()
        {
        	Assert.AreEqual(1, _systemUnderTest.ToArray().Where(activity=>activity.Equals(EXISTING_ACTIVITY_NAME)).Count());

            _systemUnderTest.Add(EXISTING_ACTIVITY_NAME);

            Assert.AreEqual(1, _systemUnderTest.ToArray().Where(activity => activity.Equals(EXISTING_ACTIVITY_NAME)).Count());
        }
    }
}