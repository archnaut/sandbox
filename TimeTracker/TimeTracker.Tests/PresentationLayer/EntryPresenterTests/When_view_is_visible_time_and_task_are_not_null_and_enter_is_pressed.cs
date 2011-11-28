// ReSharper disable InconsistentNaming
using System;
using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.Domain;
using TimeTracker.DomainLayer;
using TimeTracker.PresentationLayer;
using StructureMap.AutoMocking;

namespace TimeTracker.Tests.PresentationLayer.EntryPresenterTests
{
	[TestFixture]
	public class When_view_is_visible_time_and_task_are_not_null_and_enter_is_pressed
	{
	    const string ACTIVITY = "Some Activity";
	    const string NOTES = "Notes";
	    const string DURATION = ".5";
	    
	    private RhinoAutoMocker<TaskEntryPresenter> _container;
	
	    [SetUp]
	    public void SetUp()
	    {
	    	_container = new RhinoAutoMocker<TaskEntryPresenter>();
	    	
	    	var view = _container.Get<ITaskEntryView>();
	    	
	        view
	        	.Stub(x => x.Duration)
	        	.Return(DURATION);
	        
	        view
        		.Stub(x => x.Activity)
	        	.Return(ACTIVITY);
	        
	        view.Stub(x => x.Note).Return(NOTES);
	        view.Raise(x => x.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
	    }
	
	    [Test]
	    public void should_set_view_recent_activities()
	    {
	        //View.AssertWasCalled(view => view.SetRecentActivities(RecentActivities));
	    }
	
	    [Test]
	    public void should_update_recent_activities()
	    {
	        //RecentActivities.AssertWasCalled(recentActivities => recentActivities.Update(ACTIVITY));
	    }
	
	    [Test]
	    public void should_update_timesheet()
	    {
	        //Timesheet.AssertWasCalled(timesheet => timesheet.Update(Arg<Task>.Is.Anything));
	    }
	}
}
