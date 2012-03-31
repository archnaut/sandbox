using System;
using System.Collections.Generic;
using TimeTracker.DomainLayer;

namespace TimeTracker.PresentationLayer
{
	public interface IReportView
	{
		void Show(IEnumerable<Entry> entries);
	}
}
