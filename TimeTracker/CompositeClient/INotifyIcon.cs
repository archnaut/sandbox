using System;

namespace CompositeClient
{
	public interface INotifyIcon
	{
		event EventHandler ExitApplication;
	}
}
