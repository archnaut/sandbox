// ReSharper disable InconsistentNaming
using System;
using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracking.DomainLayer;
using TimeTracking.PresentationLayer;
using StructureMap.AutoMocking;

namespace TimeTracking.Tests.PresentationLayer.EntryPresenterTests
{
	[TestFixture]
	public class When_view_is_visible_duration_and_task_are_not_null_and_enter_is_pressed
	{
	    const string ACTIVITY = "Some Activity";
	    const string NOTE = "Notes";
	    const string DURATION = "0.5";
	    
	    private RhinoAutoMocker<EntryPresenter> _container;
	    private EntryPresenter _systemUnderTest;
	    private IEntryView _entryView;
	    private IRepository _repository;
	    private IRecentActivities _recentActivities;
	    private string[] _activities = new[]{ ACTIVITY };
	
	    [SetUp]
	    public void SetUp()
	    {
	    	_container = new RhinoAutoMocker<EntryPresenter>();
	    	_systemUnderTest = _container.ClassUnderTest;
	    	_entryView = _container.Get<IEntryView>();
	    	_repository = _container.Get<IRepository>();
	    	_recentActivities = _container.Get<IRecentActivities>();
	    	
	      	_recentActivities
	      		.Stub(x => x.First)
	      		.Return(ACTIVITY);
	      	
	      	_recentActivities
	      		.Stub(x => x.ToArray())
	      		.Return(new[]{ ACTIVITY });	    	
	        
	      	_entryView
	        	.Stub(x => x.Duration)
	        	.Return(DURATION);
	        
	        _entryView
        		.Stub(x => x.Activity)
	        	.Return(ACTIVITY);
	        
	        _entryView.Stub(x => x.Note).Return(NOTE);
	        _entryView.Raise(x => x.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
	    }
	
	    [Test]
	    public void should_set_view_recent_activities()
	    {
	    	_entryView.AssertWasCalled(view=>view.SetRecentActivities(_activities));
	    }
	
	    [Test]
	    public void should_add_entry()
	    {
	    	var entry = (Entry)_repository
	    		.GetArgumentsForCallsMadeOn(x=>x.Add(Arg<Entry>.Is.Anything))[0][0];
	    	
	    	TimeSpan duration = entry.Duration;
	    	
	    	Assert.AreEqual(ACTIVITY, entry.Activity);
	    	Assert.AreEqual(DURATION, duration.TotalHours.ToString());
	    	Assert.AreEqual(NOTE, entry.Note);
	    }
	   
	    //// ToDo: Fix Test
//	    [Test]
//	    public void should_commit_changes()
//	    {
//	    	_repository.AssertWasCalled(x => x.Commit());
//	    }
	}
}
