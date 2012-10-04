using System;
using System.Data.Entity;
using StructureMap;
using TimeTracking;
using TimeTracking.DomainLayer;
using TimeTracking.Infrastructure;
using TimeTracking.PresentationLayer;
using TimeTracking.PresentationLayer.ViewLayer;

namespace TimeTracking
{
	public class Bootstrapper : IBootstrapper
	{	
		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
		}
		
		public void BootstrapStructureMap()
		{
			ObjectFactory.Configure(x=>{
            	x.AddRegistry(new TimeTrackingRegistry());
				
				x.For<IReportPresenter>()
					.Use<ReportPresenter>();
				
				x.For<IReportView>()
					.Singleton()
					.Use<ReportView>();
				
				x.For<INotifyIcon>()
					.Singleton()
					.Use<NotifyIconView>();
				
				x.For<IEntryView>()
					.Singleton()
					.Use<EntryForm>();
				
				x.For<IEntryPresenter>()
					.Use<EntryPresenter>();
				
				x.For<IPresentationController>()
					.Use<PresentationController>();
				
//				x.For<ChordSpecification>()
//					.Use<TimeTrackerChord>();
//				
				x.For<IApplication>()
					.Use<ApplicationAdapter>();
				
				x.For<ChordSpecification>()
					.Use<ClientChord>();
			});
		}
	}
}
