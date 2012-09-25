using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace CompositeClient.TimeTracking
{
	public class TimeTrackingViewModel
	{
		private IEventAggregator _eventAggregator;
		
		public TimeTrackingViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			ExitCommand = new DelegateCommand(OnExit);
		}
		
		public DelegateCommand ExitCommand{ get; private set; }
		
		private void OnExit(){
			_eventAggregator.GetEvent<ExitEvent>().Publish(string.Empty);
		}
	}
}
