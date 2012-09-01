using System;

namespace TimeTracking
{
	public interface IPresentationController : IDisposable
	{
		event EventHandler ExitApplication;
		void ShowEntryView();
	}
}
