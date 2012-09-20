using System;

namespace CompositeClient
{
	public class NotifyIcon: INotifyIcon
	{
		private ITaskbarIcon _taskbarIcon;
		
		public event EventHandler ExitApplication;
	
		public NotifyIcon(ITaskbarIcon taskbarIcon)
		{
			_taskbarIcon = taskbarIcon;
		}
	}
}
