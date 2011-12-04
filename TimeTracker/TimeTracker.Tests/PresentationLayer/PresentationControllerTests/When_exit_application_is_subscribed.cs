// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using TimeTracker.ApplicationLayer;
using TimeTracker.PresentationLayer;

namespace TimeTracker.Tests.PresentationLayer.PresentationControllerTests
{
	[TestFixture]
	public class When_exit_application_is_subscribed
	{
	    private RhinoAutoMocker<PresentationController> _container;
	
	    [SetUp]
	    public void SetUp()
	    {
	    	_container = new RhinoAutoMocker<PresentationController>();
			
	    	_container.ClassUnderTest.ExitApplication += delegate {  };
	    }
	
	    [Test]
	    public void should_subscribe_to_notify_icon_exit_application_event()
	    {
	    	_container
	    		.Get<INotifyIcon>()
	    		.AssertWasCalled(icon=>icon.ExitApplication += Arg<EventHandler>.Is.Anything);
	    }
	}
}
