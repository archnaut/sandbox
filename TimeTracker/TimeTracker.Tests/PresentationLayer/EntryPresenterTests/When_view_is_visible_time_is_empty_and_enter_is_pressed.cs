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
	public class When_view_is_visible_time_is_empty_and_enter_is_pressed
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
	    public void should_set_focus_to_time()
	    {
			var view = _container.Get<ITaskEntryView>();
				
			view.Raise(x => x.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
	        view.AssertWasCalled(x => x.DurationSetFocus());
	    }
	}
}
