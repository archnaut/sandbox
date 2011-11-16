// ReSharper disable InconsistentNaming
using System;
using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.DomainLayer;
using TimeTracker.PresentationLayer;

namespace TimeTracker.Tests.PresentationLayer
{
    [TestFixture]
    public class TimeEntryPresenterFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            CreateSUT();
        }

        protected ITaskEntryView View { get; set; }

        protected IRecentActivities RecentActivities { get; set; }

        protected ITimesheet Timesheet { get; set; }

        protected virtual void CreateSUT()
        {
            View = MockRepository.GenerateMock<ITaskEntryView>();
            RecentActivities = MockRepository.GenerateMock<IRecentActivities>();
            Timesheet = MockRepository.GenerateMock<ITimesheet>();

            new TaskEntryPresenter(View, Timesheet, RecentActivities);
        }
    }

    public class When_time_tracker_presenter_is_constructed : TimeEntryPresenterFixture
    {
        [Test]
        public void should_subscribe_to_views_key_down_event()
        {
            View.AssertWasCalled(view => view.KeyDown += Arg<KeyEventHandler>.Is.Anything);
        }

        [Test]
        public void should_subscribe_to_views_time_text_changed_event()
        {
            View.AssertWasCalled(view => view.DurationTextChanged += Arg<EventHandler>.Is.Anything);
        }
    }

    public class When_view_is_visible_and_escape_is_pressed : TimeEntryPresenterFixture
    {
        [Test]
        public void should_hide_view()
        {
            View.Raise(view => view.KeyDown += null, this, new KeyEventArgs(Keys.Escape));

            View.AssertWasCalled(view => view.Hide());
        }
    }

    public class When_view_is_visible_time_is_empty_and_enter_is_pressed : TimeEntryPresenterFixture
    {
        [Test]
        public void should_set_focus_to_time()
        {
            View.Raise(view => view.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
            View.AssertWasCalled(view => view.DurationSetFocus());
        }
    }

    public class When_view_is_visible_time_is_not_empty_task_is_empty_and_enter_is_pressed : TimeEntryPresenterFixture
    {
        [Test]
        public void should_set_focus_to_task()
        {
            View.Stub(view => view.Duration).Return(DateTime.Now.ToShortTimeString());
            View.Raise(view => view.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
            View.AssertWasCalled(view => view.ActivitySetFocus());
        }
    }

    public class When_view_is_visible_time_and_task_are_not_null_and_enter_is_pressed : TimeEntryPresenterFixture
    {
        const string ACTIVITY = "Some Activity";
        const string NOTES = "Notes";
        const string DURATION = ".5";

        public override void SetUp()
        {

            base.SetUp();

            View.Stub(view => view.Duration).Return(DURATION);
            View.Stub(view => view.Activity).Return(ACTIVITY);
            View.Stub(view => view.Note).Return(NOTES);
            View.Raise(view => view.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
        }

        [Test]
        public void should_set_view_recent_activities()
        {
            View.AssertWasCalled(view => view.SetRecentActivities(RecentActivities));
        }

        [Test]
        public void should_update_recent_activities()
        {
            RecentActivities.AssertWasCalled(recentActivities => recentActivities.Update(ACTIVITY));
        }

        [Test]
        public void should_update_timesheet()
        {
            Timesheet.AssertWasCalled(timesheet => timesheet.Update(Arg<Task>.Is.Anything));
        }
    }

    public class When_time_changes_to_a_valid_time : TimeEntryPresenterFixture
    {
        [Test]
        public void should_do_nothing()
        {
            View.Stub(view => view.Duration).Return("0");
            View.Raise(view=>view.DurationTextChanged += null, this, EventArgs.Empty);
        }

        protected override void CreateSUT()
        {
            View = MockRepository.GenerateStrictMock<ITaskEntryView>();
            View.Stub(view => view.KeyDown += Arg<KeyEventHandler>.Is.Anything);
            View.Stub(view => view.DurationTextChanged += Arg<EventHandler>.Is.Anything);

            RecentActivities = MockRepository.GenerateMock<IRecentActivities>();
            Timesheet = MockRepository.GenerateMock<ITimesheet>();

            new TaskEntryPresenter(View, Timesheet, RecentActivities);
        }
    }
    
    public class When_time_changes_and_time_is_time_is_empty : TimeEntryPresenterFixture
    {
        [Test]
        public void should_set_time_text_to_empty_string()
        {
            View.Stub(view => view.Duration).Return("");
            View.Stub(view => view.Duration = string.Empty);

            View.Raise(view=>view.DurationTextChanged += null, this, EventArgs.Empty);
        }

        protected override void CreateSUT()
        {
            View = MockRepository.GenerateStrictMock<ITaskEntryView>();
            View.Stub(view => view.KeyDown += Arg<KeyEventHandler>.Is.Anything);
            View.Stub(view => view.DurationTextChanged += Arg<EventHandler>.Is.Anything);

            RecentActivities = MockRepository.GenerateMock<IRecentActivities>();
            Timesheet = MockRepository.GenerateMock<ITimesheet>();

            new TaskEntryPresenter(View, Timesheet, RecentActivities);
        }
    }
    
    public class When_time_changes_and_valid_time_becomes_invalid : TimeEntryPresenterFixture
    {
        [Test]
        public void should_set_time_text_to_previously_valid_time()
        {
            View.Stub(view => view.Duration).Return("10A");
            View.Stub(view => view.Duration = "10");

            View.Raise(view=>view.DurationTextChanged += null, this, EventArgs.Empty);
        }

        protected override void CreateSUT()
        {
            View = MockRepository.GenerateStrictMock<ITaskEntryView>();
            View.Stub(view => view.KeyDown += Arg<KeyEventHandler>.Is.Anything);
            View.Stub(view => view.DurationTextChanged += Arg<EventHandler>.Is.Anything);

            RecentActivities = MockRepository.GenerateMock<IRecentActivities>();
            Timesheet = MockRepository.GenerateMock<ITimesheet>();

            new TaskEntryPresenter(View, Timesheet, RecentActivities);
        }
    }
}