using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TimeTracking;
using TimeTracking.DomainLayer;
using TimeTracking.Infrastructure;

namespace TimeTracking.PresentationLayer
{
    public class EntryPresenter : IEntryPresenter
    {
        private readonly IEntryView _entryView;
        private readonly IRepository _repository;
        private readonly IRecentActivities _recentActivities;

        public EntryPresenter(IEntryView taskEntryView, IRepository repository, IRecentActivities recentActivities)
        {
            _entryView = taskEntryView;
			_repository = repository;
			_recentActivities = recentActivities;

            taskEntryView.DurationTextChanged += DurationChanged;
            taskEntryView.KeyDown += TimeTrackerViewKeyDown;
        }

		public void ShowView()
        {
            if (_entryView.Visible)
                return;

            if(_entryView.InvokeRequired)
            	_entryView.Invoke(new Action(ShowView), null);
            		
            _entryView.Clear();
            _entryView.SetLastActivity(_recentActivities.First);
            _entryView.Show(_recentActivities.ToArray());
        }

		public void Dispose()
		{
			if(_entryView.InvokeRequired)
				_entryView.Invoke(new Action(Dispose), null);
			
			_entryView.Dispose();
		}

		private void TimeTrackerViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape && e.KeyCode != Keys.Enter)
                return;

            if(e.KeyCode == Keys.Escape)
            {
                _entryView.Hide();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(_entryView.Duration))
                {
                    _entryView.DurationSetFocus();
                    e.SuppressKeyPress = true;
                    return;
                }

                if (string.IsNullOrEmpty(_entryView.Activity))
                {
                    _entryView.ActivitySetFocus();
                    e.SuppressKeyPress = true;
                    return;
                }

                _recentActivities.Add(_entryView.Activity.Trim());
                _entryView.SetRecentActivities(_recentActivities.ToArray());

                _repository.Add(CreateEntry());
                _repository.Commit();
                _entryView.Hide();

                e.SuppressKeyPress = true;
            }
        }

        private Entry CreateEntry()
        {
        	var duration = double.Parse(_entryView.Duration);
        	return new Entry(DateTime.Now, duration, _entryView.Activity, _entryView.Note);
        }

        private void DurationChanged(object sender, EventArgs e)
        {
            double duration;
            string durationText = _entryView.Duration == "." ? "0." : _entryView.Duration;

            if (double.TryParse(durationText, out duration))
                return;

            int index = durationText.Length;

            _entryView.Duration = index <= 1 ? string.Empty : durationText.Remove(index - 1);
        }
        
        private Queue<string> GetRecentActivites()
        {
			return _repository.AllInstances<Entry>()
            	.OrderByDescending(entry=>entry.Date)
				.Select(entry=>entry.Activity)
				.ToQueue();
        }
    	
    }
}