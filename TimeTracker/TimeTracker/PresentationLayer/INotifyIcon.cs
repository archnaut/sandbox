using System;

namespace TimeTracker.PresentationLayer
{
    public interface INotifyIcon : IDisposable
    {
        void Hide();
        event EventHandler ExitApplication;
        event EventHandler ShowReport;
    }
}