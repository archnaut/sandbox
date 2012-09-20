using System;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;

namespace CompositeClient
{
	public interface ITaskbarIcon
	{
		string ToolTipText{get; set;}
		Icon Icon{get; set;}
	}
}
