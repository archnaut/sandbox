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
	public class When_time_tracker_presenter_is_constructed
	{
		private RhinoAutoMocker<TaskEntryPresenter> _container;
		private TaskEntryPresenter _systemUnderTest;
		
		[SetUp]
		public void Setup()
		{
			_container = new RhinoAutoMocker<TaskEntryPresenter>();
			_systemUnderTest = _container.ClassUnderTest;
		}
		
	    [Test]
	    public void should_subscribe_to_views_key_down_event()
	    {
	    	_container
	    		.Get<ITaskEntryView>()
	       		.AssertWasCalled(x => x.KeyDown += Arg<KeyEventHandler>.Is.Anything);
	    }
	
	    [Test]
	    public void should_subscribe_to_views_time_text_changed_event()
	    {
	    	_container
	    		.Get<ITaskEntryView>()
	    		.AssertWasCalled(x => x.DurationTextChanged += Arg<EventHandler>.Is.Anything);
	    }
	}
}
