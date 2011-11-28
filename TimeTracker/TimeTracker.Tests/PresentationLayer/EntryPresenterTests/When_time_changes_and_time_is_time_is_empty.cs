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
	public class When_time_changes_and_time_is_time_is_empty
	{
		private RhinoAutoMocker<TaskEntryPresenter> _container;
		
		[SetUp]
		public void Setup()
		{
			_container = new RhinoAutoMocker<TaskEntryPresenter>();
			
			var view = _container.Get<ITaskEntryView>();
		
			view.Stub(x => x.KeyDown += Arg<KeyEventHandler>.Is.Anything);
	        view.Stub(x => x.DurationTextChanged += Arg<EventHandler>.Is.Anything);
		}
	
		[Test]
	    public void should_set_time_text_to_empty_string()
	    {
	    	var view = _container.Get<ITaskEntryView>();
	    	
	        view
	        	.Stub(x => x.Duration)
	        	.Return("");
	        
	        view.Stub(x => x.Duration = string.Empty);
	        view.Raise(x=>x.DurationTextChanged += null, this, EventArgs.Empty);
	    }
	}
}
