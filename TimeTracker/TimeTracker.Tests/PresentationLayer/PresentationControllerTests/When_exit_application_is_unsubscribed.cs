// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using TimeTracking;
using TimeTracking.PresentationLayer;

namespace TimeTracking.Tests.PresentationLayer.PresentationControllerTests
{
	[TestFixture]
	public class When_exit_application_is_unsubscribed
	{
	    private RhinoAutoMocker<PresentationController> _container;
	    
	    [SetUp]
	    public void SetUp()
	    {
	    	_container = new RhinoAutoMocker<PresentationController>();
	    	
	    	_container.ClassUnderTest.ExitApplication -= delegate{};
	    }
	
	    [Test]
	    public void should_unsubscribe_from_notify_icon_exit_application_event()
	    {
	    	_container
	    		.Get<INotifyIcon>()
	        	.AssertWasCalled(icon => icon.ExitApplication -= Arg<EventHandler>.Is.Anything);
	    }
	}    
}
