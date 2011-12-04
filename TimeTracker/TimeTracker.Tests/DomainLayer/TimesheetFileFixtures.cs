// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.DomainLayer;

namespace TimeTracker.Tests.DomainLayer
{
    [TestFixture]
    public class When_does_not_exist_and_timesheet_is_asked_for_last_task
    {
        private IFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = MockRepository.GenerateStub<IFile>();
            _file.Stub(file => file.Exists()).Return(false);
        }

        [Test]
        public void should_return_empty_task()
        {
            Timesheet sut = new Timesheet(_file);
            Assert.AreEqual(new Task(), sut.GetLastTask());
        }
    }

    [TestFixture]
    public class When_get_last_task_is_called_and_file_contains_one_task
    {
        private IFile _file;
        private Task _task;
        private DateTime _taskDate;
        private const string TASK_DURATION = "3.75";

        [SetUp]
        public void SetUp()
        {
            _taskDate = DateTime.Parse(DateTime.Now.ToString());

            _task = new Task(_taskDate, TASK_DURATION, "Some Activity", "Some Note");
            _file = MockRepository.GenerateStub<IFile>();
            _file.Stub(file => file.Exists()).Return(true);
            _file
                .Stub(file => file.ReadAllLines())
                .Return(new[] { string.Format("{0},{1},{2},\"{3}\",\"{4}\"", _task.Date, _task.Date.DayOfWeek, TASK_DURATION, _task.Activity, _task.Note) });
        }

        [Test]
        public void should_return_task_contained_in_file()
        {
            Timesheet timesheet = new Timesheet(_file);
            Assert.AreEqual(_task, timesheet.GetLastTask());
        }
    }

    [TestFixture]
    public class When_get_last_task_is_called_and_file_contains_many_tasks
    {
        private IFile _file;
        private Task _task;
        private DateTime _taskDate;

        [SetUp]
        public void SetUp()
        {
            _taskDate = DateTime.Parse(DateTime.Now.ToString());

            _task = new Task(_taskDate, "3.75", "Some Activity", "Some Note");
            _file = MockRepository.GenerateStub<IFile>();
            _file.Stub(file => file.Exists()).Return(true);
            _file
                .Stub(file => file.ReadAllLines())
                .Return(new[]
                            {
                                string.Format("{0},{1},{2},\"{3}\",\"{4}\"", _task.Date.AddDays(-2), _task.Date.DayOfWeek, _task.Duration.TotalHours, _task.Activity, _task.Note),
                                string.Format("{0},{1},{2},\"{3}\",\"{4}\"", _task.Date.AddDays(-3), _task.Date.DayOfWeek, _task.Duration.TotalHours, _task.Activity, _task.Note),
                                string.Format("{0},{1},{2},\"{3}\",\"{4}\"", _task.Date.AddDays(-1), _task.Date.DayOfWeek, _task.Duration.TotalHours, _task.Activity, _task.Note),
                                string.Format("{0},{1},{2},\"{3}\",\"{4}\"", _task.Date, _task.Date.DayOfWeek, _task.Duration.TotalHours, _task.Activity, _task.Note)
                            });
        }

        [Test]
        public void should_return_most_recent_task()
        {
            Timesheet timesheet = new Timesheet(_file);
            Assert.AreEqual(_task, timesheet.GetLastTask());
        }
    }

    [TestFixture]
    public class When_timesheet_is_updated
    {
        private IFile _file;
        private Task _task;
        private DateTime _taskDate;

        [SetUp]
        public void SetUp()
        {
            _taskDate = DateTime.Parse(DateTime.Now.ToString());
            _task = new Task(_taskDate, "3.75", "Some Activity", "Some Note");
            _file = MockRepository.GenerateMock<IFile>();
        }

        [Test]
        public void should_append_task_to_file()
        {
            Timesheet timesheet = new Timesheet(_file);
            timesheet.Update(_task);

            string content = string.Format("{0},{1},{2},\"{3}\",\"{4}\"{5}", _taskDate, _taskDate.DayOfWeek, _task.Duration.TotalHours, _task.Activity, _task.Note, Environment.NewLine);

            _file.AssertWasCalled(file=>file.AppendAllText(content));
        }
    }
}