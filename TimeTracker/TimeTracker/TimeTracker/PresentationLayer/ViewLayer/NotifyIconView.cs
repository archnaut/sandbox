using System;
using System.Windows.Forms;
using TimeTracker.Properties;

namespace TimeTracker.PresentationLayer.ViewLayer
{
    public class NotifyIconView : INotifyIcon
    {
        public event EventHandler ExitApplication = delegate { };

        private readonly NotifyIcon _notifyIcon = new NotifyIcon();

        public NotifyIconView()
        {
            _notifyIcon.Icon = Resources.StopWatch;
            _notifyIcon.ContextMenu = new ContextMenu(new[] { new MenuItem("Exit", OnExit) });
            _notifyIcon.Visible = true;
        }

        public void Hide()
        {
            _notifyIcon.Visible = false;
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        private void OnExit(object sender, EventArgs args)
        {
            ExitApplication(this, EventArgs.Empty);
        }
    }
}