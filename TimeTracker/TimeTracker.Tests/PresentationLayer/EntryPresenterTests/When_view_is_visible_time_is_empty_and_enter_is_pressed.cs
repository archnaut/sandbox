// ReSharper disable InconsistentNaming
using System;
using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.DomainLayer;
using TimeTracker.PresentationLayer;
using StructureMap.AutoMocking;

namespace TimeTracker.Tests.PresentationLayer.EntryPresenterTests
{
		[TestFixture]
	public class When_view_is_visible_time_is_empty_and_enter_is_pressed
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
			
			_entryView.Raise(x => x.KeyDown += null, this, new KeyEventArgs(Keys.Enter));
		}
		
	    [Test]
	    public void should_set_focus_to_time()
	    {
	        _entryView.AssertWasCalled(x => x.DurationSetFocus());
	    }
	}
}
