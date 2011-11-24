using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TimeTracker.ApplicationLayer;
using TimeTracker.Domain;
using TimeTracker.DomainLayer;

namespace TimeTracker.PresentationLayer
{
    public class TaskEntryPresenter : ITaskEntryPresenter
    {
        private readonly ITaskEntryView _taskEntryView;
        private readonly IRepository _repository;
        private List<string> _recentActivities;

        public TaskEntryPresenter(ITaskEntryView taskEntryView, IRepository repository)
        {
            _taskEntryView = taskEntryView;
			_repository = repository;

            taskEntryView.DurationTextChanged += DurationChanged;
            taskEntryView.KeyDown += TimeTrackerViewKeyDown;
        }

        private void TimeTrackerViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape && e.KeyCode != Keys.Enter)
                return;

            if(e.KeyCode == Keys.Escape)
            {
                _taskEntryView.Hide();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(_taskEntryView.Duration))
                {
                    _taskEntryView.DurationSetFocus();
                    e.SuppressKeyPress = true;
                    return;
                }

                if (string.IsNullOrEmpty(_taskEntryView.Activity))
                {
                    _taskEntryView.ActivitySetFocus();
                    e.SuppressKeyPress = true;
                    return;
                }

                _recentActivities.Insert(0, _taskEntryView.Activity.Trim());
                _recentActivities = _recentActivities.Take(10).ToList();
                _taskEntryView.SetRecentActivities(_recentActivities);

                _repository.Add(CreateEntry());
                _repository.Commit();
                _taskEntryView.Hide();

                e.SuppressKeyPress = true;
            }
        }

        private Entry CreateEntry()
        {
        	return new Entry(DateTime.Now, _taskEntryView.Duration, _taskEntryView.Activity, _taskEntryView.Note);
        }

        private void DurationChanged(object sender, EventArgs e)
        {
            double duration;
            string durationText = _taskEntryView.Duration == "." ? "0." : _taskEntryView.Duration;

            if (double.TryParse(durationText, out duration))
                return;

            int index = durationText.Length;

            _taskEntryView.Duration = index <= 1 ? string.Empty : durationText.Remove(index - 1);
        }

        public void ShowView()
        {
            if (_taskEntryView.Visible)
                return;

            _recentActivities = _recentActivities ?? GetRecentActivites();
            
            _taskEntryView.Clear();
            _taskEntryView.SetLastActivity(_recentActivities.FirstOrDefault());
            _taskEntryView.Show(_recentActivities.ToArray());
        }
        
        private List<string> GetRecentActivites()
        {
			return _repository.AllInstances<Entry>()
            	.OrderByDescending(entry=>entry.Date)
				.Select(entry=>entry.Activity)
				.ToList();
        }
    	
		public void Dispose()
		{
			_taskEntryView.Dispose();
		}
    }
}