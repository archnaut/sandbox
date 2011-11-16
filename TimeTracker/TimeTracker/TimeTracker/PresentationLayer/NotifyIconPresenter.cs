using System;
using TimeTracker.ApplicationLayer;

namespace TimeTracker.PresentationLayer
{
    public class NotifyIconPresenter : IApplicationExit, IDisposable
    {
        private readonly INotifyIcon _notifyIcon;

        public NotifyIconPresenter(INotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;
        }

        public event EventHandler ExitApplication
        {
            add { _notifyIcon.ExitApplication += value; }
            remove { _notifyIcon.ExitApplication -= value; }
        }

        public void Dispose()
        {
            _notifyIcon.Hide();
            _notifyIcon.Dispose();
        }
    }
}