using System;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using TimeTracker.PresentationLayer;

namespace TimeTracker.Tests.PresentationLayer.PresentationControllerTests
{
	[TestFixture]
	public class When_show_report_event_is_raised
	{
		private RhinoAutoMocker<PresentationController> _container;
		
		[SetUp]
		public void Setup()
		{
			_container = new RhinoAutoMocker<PresentationController>();
			
			var systemUnderTest = _container.ClassUnderTest;
			
			_container
				.Get<INotifyIcon>()
				.Raise(x => x.ShowReport += null, this, EventArgs.Empty);
		}
		
		[Test]
		public void Should_Show_Report()
		{
			_container
				.Get<IReportPresenter>()
				.AssertWasCalled(x=>x.ShowView());
		}
	}
}
