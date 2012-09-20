using System;
using TimeTracking;

namespace CompositeClient
{
	public class PresentationController : IPresentationController
	{
		private INotifyIcon _notifyIcon;
		
		public event EventHandler ExitApplication
		{
			add{_notifyIcon.ExitApplication += value;}
			remove{_notifyIcon.ExitApplication -= value;}
		}

		public PresentationController(INotifyIcon notifyIcon)
		{
			_notifyIcon = notifyIcon;
		}
		
		public void ShowEntryView()
		{
			throw new NotImplementedException();
		}
		
		public void Dispose()
		{
		}
	}
}
