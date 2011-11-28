using System;
using TimeTracker.ApplicationLayer;

namespace TimeTracker.PresentationLayer
{
	public class PresentationController : IPresentationController
	{
		private ITaskEntryPresenter _entryPresenter;
		private IReportPresenter _reportPresenter;
		private INotifyIcon _notifyIcon;
				
		public PresentationController(ITaskEntryPresenter entryPresenter, IReportPresenter reportPresenter, INotifyIcon notifiyIcon)
		{
			_entryPresenter = entryPresenter;
			_reportPresenter = reportPresenter;
			_notifyIcon = notifiyIcon;
			
			_notifyIcon.ShowReport += OnShowReport;
		}
		
		public void ShowEntryView()
		{
			_entryPresenter.ShowView();
		}
		
        public event EventHandler ExitApplication
        {
            add { _notifyIcon.ExitApplication += value; }
            remove { _notifyIcon.ExitApplication -= value; }
        }
        
        private void OnShowReport(object sender, EventArgs args)
		{
			_reportPresenter.ShowView();
		}
		
		public void Dispose()
		{
			_entryPresenter.Dispose();
			_reportPresenter.Dispose();
			
			_notifyIcon.Hide();
			_notifyIcon.Dispose();
		}
	}
}
