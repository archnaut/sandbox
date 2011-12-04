using System;
using System.Collections.Generic;
using System.Linq;

using TimeTracker.Domain;

namespace TimeTracker.DomainLayer
{
	public class RecentActivities : IRecentActivities
	{
		private IRepository _repository;
		private List<string> _queue;
		
		public RecentActivities(IRepository repository)
		{
			_repository = repository;
		}

		public void Add(string activity)
		{
			LoadActivities();
				
			if(_queue.Contains(activity))
				_queue.Remove(activity);
			
			_queue.Insert( 0, activity );
			_queue = _queue.Take(10).ToList();
		}

		public string[] ToArray()
		{
			LoadActivities();

			return _queue.ToArray();
		}

		private void LoadActivities()
		{
			if (_queue != null)
				return;

			_queue = GetActivities();
		}

		private List<string> GetActivities()
		{
			return _repository
				.AllInstances<Entry>()
				.OrderByDescending(entry => entry.Date)
				.Take(10)
				.Select(entry=>entry.Activity)
				.ToList();
		}
		
		public string First{
			get {
				return _queue.First();
			}
		}
		
	}
}
