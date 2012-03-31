using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TimeTracker.PresentationLayer.ViewLayer
{
	public partial class ReportView : Form, IReportView
	{
		public ReportView()
		{
			InitializeComponent();
		}
				
		public void Show(IEnumerable<TimeTracker.DomainLayer.Entry> entries)
		{
			gridBindingSource.DataSource = entries.ToList();
			Show();
		}
	}
}
