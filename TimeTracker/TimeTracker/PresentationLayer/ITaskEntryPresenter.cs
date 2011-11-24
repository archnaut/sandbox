using System;

namespace TimeTracker.PresentationLayer
{
	public interface ITaskEntryPresenter : IDisposable
    {
        void ShowView();
    }
}