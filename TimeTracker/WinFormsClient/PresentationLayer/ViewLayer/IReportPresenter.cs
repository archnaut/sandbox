using System;

namespace TimeTracking.PresentationLayer
{
	public interface IReportPresenter : IDisposable
	{
		void ShowView();
	}
}
