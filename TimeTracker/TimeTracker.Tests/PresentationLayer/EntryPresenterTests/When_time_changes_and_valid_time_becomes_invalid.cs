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
	public class When_time_changes_and_valid_time_becomes_invalid
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
	    public void should_set_time_text_to_previously_valid_time()
	    {
	    	var view = _container.Get<ITaskEntryView>();
	    	
	        view
	        	.Stub(x => x.Duration)
	        	.Return("10A");
	        
	        view.Stub(x => x.Duration = "10");
	        view.Raise(x=>x.DurationTextChanged += null, this, EventArgs.Empty);
	    }
	}
}
