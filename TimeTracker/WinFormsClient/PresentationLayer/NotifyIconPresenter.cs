using System;
using TimeTracker.ApplicationLayer;

namespace TimeTracker.PresentationLayer
{
    public class NotifyIconPresenter : IDisposable
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
        
        public event EventHandler ShowReport
        {
        	add{ _notifyIcon.ShowReport += value; }
        	remove{ _notifyIcon.ShowReport -= value; }
        }

        public void Dispose()
        {
            _notifyIcon.Hide();
            _notifyIcon.Dispose();
        }
    }
}