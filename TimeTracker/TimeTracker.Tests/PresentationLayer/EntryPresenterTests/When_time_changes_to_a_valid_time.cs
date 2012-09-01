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
	public class When_duration_changes_to_a_valid_duration
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
	        	.Return("0");
	        
	        _entryView.Raise(x=>x.DurationTextChanged += null, this, EventArgs.Empty);
		}
	
		[Test]
	    public void should_do_nothing()
	    {
	    	_entryView.AssertWasNotCalled(x=>x.Duration = Arg<string>.Is.Anything);
	    }
	}
}
