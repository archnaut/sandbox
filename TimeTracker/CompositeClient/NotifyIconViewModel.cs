using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace CompositeClient
{
	public class NotifyIconViewModel : NotificationObject, INotifyIcon
	{
		public event EventHandler ExitApplication = delegate{};

		public NotifyIconViewModel()
		{
			ExitCommand = new DelegateCommand(OnExit, CanExit);
		}
		
		public DelegateCommand ExitCommand{get; private set;}
		
		private bool CanExit()
		{
			return true;
		}
		
		private void OnExit()
		{
			ExitApplication(this, EventArgs.Empty);
		}
	}
}
