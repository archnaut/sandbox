using System;

namespace TimeTracker.DomainLayer
{
    public struct Task
    {
        private readonly string _activity;
        private readonly DateTime _date;
        private readonly TimeSpan _duration;
        private readonly string _note;

        public Task(DateTime date, string duration, string activity, string note)
        {
            _date = date;
            _activity = activity;
            _note = note;

            var seconds = (int) (float.Parse(duration)*3600);
            _duration = new TimeSpan(0, 0, 0, seconds);
        }

        public string Activity
        {
            get { return _activity ?? "Unknown"; }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
        }

        public string Note
        {
            get { return _note; }
        }
    }
}