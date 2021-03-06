﻿
using System;

namespace TimeTracking.DomainLayer
{
	/// <summary>
	/// Description of Entry.
	/// </summary>
	public class Entry
	{
        private string _activity;

        private Entry(){}
        
        public Entry(DateTime date, double duration, string activity, string note)
        {
            Date = date;
            Activity = activity;
            Duration = new Duration(duration);
            Note = note;
        }
		
        public int EntryID{get; private set;}
        
        public string Activity
        {
            get { return _activity ?? "Unknown"; }
            private set{_activity = value;}
        }

        public DateTime Date{get; private set;}
        
        public Duration Duration{get; private set;}

        public string Note{get; private set;}
	}
}
