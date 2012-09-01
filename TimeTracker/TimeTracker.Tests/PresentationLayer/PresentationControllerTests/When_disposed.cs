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
    public class When_disposed
    {
        private RhinoAutoMocker<PresentationController> _container;

        [SetUp]
        public void SetUp()
        {
        	_container = new RhinoAutoMocker<PresentationController>();
        	
        	_container.ClassUnderTest.Dispose();
        }

        [Test]
        public void should_hide_notify_icon()
        {
            _container
            	.Get<INotifyIcon>()
            	.AssertWasCalled(icon => icon.Hide());
        }

        [Test]
        public void should_dispose_notify_icon()
        {
        	_container
        		.Get<INotifyIcon>()
        		.AssertWasCalled(icon=>icon.Dispose());
        }
        
        [Test]
        public void should_dispose_report_presenter()
        {
        	_container
        		.Get<IReportPresenter>()
        		.AssertWasCalled(icon=>icon.Dispose());
        }
        
        [Test]
        public void should_dispose_entry_presenter()
        {
        	_container
        		.Get<INotifyIcon>()
        		.AssertWasCalled(icon=>icon.Dispose());
        }
    }
}