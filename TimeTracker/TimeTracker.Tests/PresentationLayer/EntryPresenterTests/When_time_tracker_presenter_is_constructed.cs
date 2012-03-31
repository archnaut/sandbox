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
	public class When_entry_presenter_is_constructed
	{
		private RhinoAutoMocker<EntryPresenter> _container;
		private EntryPresenter _systemUnderTest;
		
		[SetUp]
		public void Setup()
		{
			_container = new RhinoAutoMocker<EntryPresenter>();
			_systemUnderTest = _container.ClassUnderTest;
		}
		
	    [Test]
	    public void should_subscribe_to_views_key_down_event()
	    {
	    	_container
	    		.Get<IEntryView>()
	       		.AssertWasCalled(x => x.KeyDown += Arg<KeyEventHandler>.Is.Anything);
	    }
	
	    [Test]
	    public void should_subscribe_to_views_duration_text_changed_event()
	    {
	    	_container
	    		.Get<IEntryView>()
	    		.AssertWasCalled(x => x.DurationTextChanged += Arg<EventHandler>.Is.Anything);
	    }
	}
}
