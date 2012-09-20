using System;
using System.Windows.Forms;
using TimeTracking.Properties;

namespace TimeTracking.PresentationLayer.ViewLayer
{
    public class NotifyIconView : INotifyIcon
    {
        public event EventHandler ExitApplication = delegate { };
        public event EventHandler ShowReport = delegate{};

        private readonly NotifyIcon _notifyIcon = new NotifyIcon();

        public NotifyIconView()
        {
            _notifyIcon.Icon = Resources.StopWatchIcon;
            
            _notifyIcon.ContextMenu = new ContextMenu(new[] {
				new MenuItem("Exit", OnExit),
				new MenuItem("Report", OnShowReport)
			});
            
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
        
        private void OnShowReport(object sender, EventArgs args)
        {
        	ShowReport(this, EventArgs.Empty);
        }
    }
}