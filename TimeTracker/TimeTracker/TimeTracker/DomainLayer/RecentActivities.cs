using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.DomainLayer
{
    public class RecentActivities : IRecentActivities
    {
        private readonly IFile _file;
        private List<Activity> _activities;

        public RecentActivities(IFile file)
        {
            _file = file;
        }

        public void Update(string activityName)
        {
            if (_activities == null)
                _activities = LoadActivities();

            Update(new Activity(activityName, Environment.TickCount));
            SaveActivities();
        }

        public string[] ToArray()
        {
            return GetTasks().ToArray();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return GetTasks().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static Activity CreateActivity(string line)
        {
            string[] record = line.Split('|');
            return new Activity(record[0], long.Parse(record[1]));
        }

        private List<Activity> LoadActivities()
        {
            if (!_file.Exists()) 
                return new List<Activity>();
            
            return _file
                .ReadAllLines()
                .Select(line => CreateActivity(line))
                .ToList();
        }

        private IEnumerable<string> GetTasks()
        {
            if (_activities == null)
                _activities = LoadActivities();

            return _activities.OrderByDescending(activity => activity.Timestamp).Select(task => task.ToString());
        }

        private void Update(Activity activity)
        {
            int index = _activities.FindIndex(x => x.ActivityName.Equals(activity.ActivityName));

            if (index != -1)
                _activities.RemoveAt(index);

            _activities.Insert(0, activity);
        }

        private void SaveActivities()
        {
            string[] content = _activities
                .Take(10)
                .Select(activity=>string.Format("{0}|{1}", activity.ActivityName, activity.Timestamp))
                .ToArray();

            _file.WriteAllLines(content);
        }
    }
}