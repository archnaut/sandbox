using System;

namespace TimeTracker.ApplicationLayer
{
	public interface IPresentationController : IDisposable
	{
		event EventHandler ExitApplication;
		void ShowEntryView();
	}
}
