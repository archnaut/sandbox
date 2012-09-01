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
	public class When_view_is_visible_time_is_not_empty_task_is_empty_and_enter_is_pressed
	{
		private RhinoAutoMocker<EntryPresenter> _container;
		private EntryPresenter _systemUnderTest;
		private IEntryView _entryView;
		
		[SetUp]
		public void Setup()
		{
			_container = new RhinoAutoMocker<EntryPresenter>();
			_systemUnderTest = _container.ClassUnderTest;
	    	_entryView = _container.Get<IEntryView>();
	    	
	    	_entryView
	    		.Stub(x => x.Duration)
	    		.Return(DateTime.Now.ToShortTimeString());
	    	
	        _entryView.Raise(x => x.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
		}
	
		[Test]
	    public void should_set_focus_to_task()
	    {
	        _entryView.AssertWasCalled(x => x.ActivitySetFocus());
	    }
	}
}
