using System;
using System.Collections.Generic;
using TimeTracking.DomainLayer;

namespace TimeTracking.PresentationLayer
{
	public interface IReportView
	{
		void Show(IEnumerable<Entry> entries);
	}
}
