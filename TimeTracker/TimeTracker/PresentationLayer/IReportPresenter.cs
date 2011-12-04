using System;

namespace TimeTracker.PresentationLayer
{
	public interface IReportPresenter : IDisposable
	{
		void ShowView();
	}
}
