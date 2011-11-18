
using System;

namespace TimeTracker.DomainLayer
{
	/// <summary>
	/// Description of Entry.
	/// </summary>
	public class Entry
	{
        private string _activity;

        public Entry(DateTime date, string duration, string activity, string note)
        {
            Date = date;
            Activity = activity;
            Note = note;

            var seconds = (int) (float.Parse(duration)*3600);
            Duration = new TimeSpan(0, 0, 0, seconds);
        }

        public string Activity
        {
            get { return _activity ?? "Unknown"; }
            private set{_activity = value;}
        }

        public DateTime Date{get; private set;}

        public TimeSpan Duration{get; private set;}

        public string Note{get; private set;}
	}
}
