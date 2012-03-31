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
	public class When_duration_changes_and_valid_duration_becomes_invalid
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
	        	.Return("10A");
			
			_entryView.Raise(x=>x.DurationTextChanged += null, this, EventArgs.Empty);
		}
	
		[Test]
	    public void should_set_duration_text_to_previously_valid_duration()
	    {
	        _entryView.AssertWasCalled(x => x.Duration = "10");
	    }
	}
}
