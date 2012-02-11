using System;

namespace TimeTracker.PresentationLayer
{
	public interface IEntryPresenter : IDisposable
    {
        void ShowView();
    }
}