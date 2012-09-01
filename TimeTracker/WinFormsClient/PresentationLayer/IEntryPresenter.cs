using System;

namespace TimeTracking.PresentationLayer
{
	public interface IEntryPresenter : IDisposable
    {
        void ShowView();
    }
}