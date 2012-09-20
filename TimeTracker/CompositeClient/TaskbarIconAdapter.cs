using System;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;

namespace CompositeClient
{
	public class TaskbarIconAdapter : ITaskbarIcon
	{
		private readonly TaskbarIcon _taskbarIcon = new TaskbarIcon();
		
		public TaskbarIconAdapter(ITrayPopupView trayPopupView)
		{
			_taskbarIcon.TrayPopup = trayPopupView.ToUIElement();
		}
		
		public string ToolTipText
		{
			get{return _taskbarIcon.ToolTipText;}
			set{_taskbarIcon.ToolTipText = value;}
		}
		
		public Icon Icon
		{
			get{return _taskbarIcon.Icon;}
			set{_taskbarIcon.Icon = value;}
		}
	}
}
