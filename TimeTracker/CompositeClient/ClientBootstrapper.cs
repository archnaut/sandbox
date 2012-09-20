using System;
using System.Windows;
using CompositeClient.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using TimeTracking;
using TimeTracking.Properties;

namespace CompositeClient
{
	public class ClientBootStrapper : StructureMapBootstrapper
	{
		public static void Bootstrap()
		{
			var bootstrapper = new ClientBootStrapper();
			bootstrapper.Run();			
		}
				
		protected override System.Windows.DependencyObject CreateShell()
		{
			return new Shell();
		}
		
		 protected override void InitializeShell()
        {
            base.InitializeShell();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(TimeTracking.TimeTrackingModule));
        }
        
		public override void Run(bool runWithDefaultConfiguration)
		{
			base.Run(runWithDefaultConfiguration);
				
			ConfigureContainer(x=>{
			    x.IncludeRegistry<TimeTrackingRegistry>();
				x.For<ITrayPopupView>().Use<TrayPopupView>();
				x.For<IApplication>().Use<ClientApplication>();
				x.For<IPresentationController>().Use<PresentationController>();
				
				x.For<ITaskbarIcon>()
					.Use<TaskbarIconAdapter>()
					.OnCreation(taskbarIcon=>taskbarIcon.Icon = Resources.StopWatchIcon);
				
				x.For<INotifyIcon>().Use<NotifyIcon>();
				x.For<ChordSpecification>().Use<ClientChord>();
          	});
		}
	}
}
