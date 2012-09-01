using System;
using TimeTracking;
using TimeTracking.DomainLayer;

namespace TimeTracking.PresentationLayer
{
	public class ReportPresenter : IReportPresenter
	{
		IReportView _view;
		IRepository _repository;
		
		public ReportPresenter(IReportView view, IRepository repository)
		{
			_view = view;
			_repository = repository;
		}
		
		public void ShowView()
		{
			var _entries = _repository.AllInstances<Entry>();
			_view.Show(_entries);
		}
		
		public void Dispose()
		{
		}
	}
}
