using System;
using System.Windows.Forms;
using TimeTracker.DomainLayer;

namespace TimeTracker.PresentationLayer
{
    public class TaskEntryPresenter : ITaskEntryPresenter
    {
        private readonly IRecentActivities _recentActivities;
        private readonly ITimesheet _timeSheet;
        private readonly ITaskEntryView _taskEntryView;

        public TaskEntryPresenter(ITaskEntryView taskEntryView, ITimesheet timesheet, IRecentActivities recentActivities)
        {
            _taskEntryView = taskEntryView;
            _timeSheet = timesheet;
            _recentActivities = recentActivities;

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

                _recentActivities.Update(_taskEntryView.Activity.Trim());
                _taskEntryView.SetRecentActivities(_recentActivities);

                _timeSheet.Update(CreateTask());
                _taskEntryView.Hide();

                e.SuppressKeyPress = true;
            }
        }

        private Task CreateTask()
        {
            return new Task(DateTime.Now, _taskEntryView.Duration ,_taskEntryView.Activity, _taskEntryView.Note);
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

            _taskEntryView.Clear();
            _taskEntryView.SetLastActivity(_timeSheet.GetLastTask().Activity);
            _taskEntryView.Show(_recentActivities.ToArray());
        }
    }
}