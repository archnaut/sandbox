// ReSharper disable InconsistentNaming
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.DomainLayer;

namespace TimeTracker.Tests.DomainLayer
{
    [TestFixture]
    public class When_activity_is_added_to_empty_recent_activities
    {
        private const string FIRST_ACTIVITY = "First Activity";
        private IFile _file;
        private RecentActivities _recentActivities;

        [SetUp]
        public void SetUp()
        {
            _file = MockRepository.GenerateMock<IFile>();
            _recentActivities = new RecentActivities(_file);

            _recentActivities.Update(FIRST_ACTIVITY);
        }

        [Test]
        public void activity_added_should_be_first_activity()
        {

            Assert.AreEqual(FIRST_ACTIVITY, _recentActivities.First());
        }

        [Test]
        public void activity_added_should_be_last_activity()
        {
            Assert.AreEqual(FIRST_ACTIVITY, _recentActivities.Last());
        }

        [Test]
        public void should_save_new_activity_to_file()
        {
            _file.AssertWasCalled(file=>file.WriteAllLines(Arg<string[]>.Matches(lines=>lines[0].Contains(FIRST_ACTIVITY))));
        }
       
    }

    [TestFixture]
    public class When_activity_is_added_to_recent_activities_containing_one
    {
        private const string FIRST_ACTIVITY_NAME = "First Activity";
        private const string SECOND_ACTIVITY_NAME = "Second Activity";
        private IFile _file;
        private RecentActivities _recentActivities;

        [SetUp]
        public void SetUp()
        {
            _file = MockRepository.GenerateMock<IFile>();
            _recentActivities = new RecentActivities(_file);

            _recentActivities.Update(FIRST_ACTIVITY_NAME);
            _recentActivities.Update(SECOND_ACTIVITY_NAME);
        }

        [Test]
        public void activity_added_should_be_first_activity()
        {
            Assert.AreEqual(SECOND_ACTIVITY_NAME, _recentActivities.First());
        }

        [Test]
        public void first_activity_added_should_be_last_activity()
        {
            Assert.AreEqual(FIRST_ACTIVITY_NAME, _recentActivities.Last());
        }

        [Test]
        public void should_save_new_activities_to_file()
        {
            _file.AssertWasCalled(file=>file.WriteAllLines(Arg<string[]>.Matches(lines=>ContainsActivities(lines))));
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
                                                        "Tenth Activity",
                                                        "Last Activity"
                                                    };

        private IFile _file;
        private RecentActivities _recentActivities;

        [SetUp]
        public void SetUp()
        {
            _file = MockRepository.GenerateStub<IFile>();
            _recentActivities = new RecentActivities(_file);

            foreach(string activity in _activities)
                _recentActivities.Update(activity);
        }

        [Test]
        public void Last_activity_added_should_be_first()
        {
            Assert.AreEqual(_activities[10], _recentActivities.First());
        }

        [Test]
        public void First_activity_added_should_be_last()
        {
            Assert.AreEqual(_activities[0], _recentActivities.Last());
        }

        [Test]
        public void should_save_ten_most_recent_activities_to_file()
        {
            _file.AssertWasCalled(file => file.WriteAllLines(Arg<string[]>.Matches(content=>ContainsActivities(content))));
        }

        private bool ContainsActivities(string[] content)
        {
            if (content.Length < 10)
                return false;

            return content.Length == 10 && content[0].Contains(_activities[10]) && content[9].Contains(_activities[1]);
        }
    }

    [TestFixture]
    public class When_existing_activity_is_added_to_recent_activities
    {
        private const string EXISTING_ACTIVITY_NAME = "Existing Activity";
        private const string SECOND_ACTIVITY_NAME = "Second Activity";
        private const string THIRD_ACTIVITY_NAME = "Third Activity";

        private IFile _file;
        private RecentActivities _recentActivities;

        [SetUp]
        public void SetUp()
        {
            _file = MockRepository.GenerateStub<IFile>();
            _recentActivities = new RecentActivities(_file);

            _recentActivities.Update(EXISTING_ACTIVITY_NAME);
            _recentActivities.Update(SECOND_ACTIVITY_NAME);
            _recentActivities.Update(THIRD_ACTIVITY_NAME);
        }

        [Test]
        public void existing_activity_should_become_first()
        {
            Assert.AreEqual(THIRD_ACTIVITY_NAME, _recentActivities.First());

            _recentActivities.Update(EXISTING_ACTIVITY_NAME);

            Assert.AreEqual(EXISTING_ACTIVITY_NAME, _recentActivities.First());

        }

        [Test]
        public void should_contain_stingle_instance_of_existing_activity()
        {
            Assert.AreEqual(1, _recentActivities.Where(activity=>activity.Equals(EXISTING_ACTIVITY_NAME)).Count());

            _recentActivities.Update(EXISTING_ACTIVITY_NAME);

            Assert.AreEqual(1, _recentActivities.Where(activity => activity.Equals(EXISTING_ACTIVITY_NAME)).Count());
        }
    }
}