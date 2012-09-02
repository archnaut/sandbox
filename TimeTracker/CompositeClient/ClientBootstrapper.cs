using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;

namespace CompositeClient
{
	public class ClientBootStrapper : StructureMapBootstrapper
	{
		public static void Bootstrap()
		{
			new ClientBootStrapper().Run();
		}
		
		protected override System.Windows.DependencyObject CreateShell()
		{
			return new Shell();
		}
		
		 protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(TimeTracking.TimeTrackingModule));
        }		 
	}
}
