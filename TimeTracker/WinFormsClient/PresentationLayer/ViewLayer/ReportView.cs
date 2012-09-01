using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TimeTracking.PresentationLayer.ViewLayer
{
	public partial class ReportView : Form, IReportView
	{
		public ReportView()
		{
			InitializeComponent();
		}
				
		public void Show(IEnumerable<TimeTracking.DomainLayer.Entry> entries)
		{
			gridBindingSource.DataSource = entries.ToList();
			Show();
		}
	}
}
