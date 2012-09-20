using System;
using System.Windows;
using TimeTracking;

namespace CompositeClient
{
	public class ClientApplication : Application, IApplication
	{
		private ITaskbarIcon _taskbarIcon;
		
		public ClientApplication(ITaskbarIcon taskbarIcon)
		{
			_taskbarIcon = taskbarIcon;
		}
		
		void IApplication.Run()
		{
			Run();
		}
		
		void IApplication.Exit()
		{
			Shutdown();
		}
	}
}
