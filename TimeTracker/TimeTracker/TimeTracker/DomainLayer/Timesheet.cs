using System;
using System.Linq;
using TimeTracker.Infrastructure;

namespace TimeTracker.DomainLayer
{
    public class Timesheet : ITimesheet
    {
        private readonly IFile _file;

        public Timesheet(string fileName):this(new FileAdapter(fileName))
        {}

        internal Timesheet(IFile file)
        {
            _file = file;
        }

        public void Update(Task task)
        {
            string content = string.Format("{0},{1},{2},\"{3}\",\"{4}\"{5}", 
                                           task.Date, task.Date.DayOfWeek, task.Duration.TotalHours, task.Activity, task.Note, Environment.NewLine);

            _file.AppendAllText(content);
        }

        public Task GetLastTask()
        {
            if (!_file.Exists())
                return new Task();

            return _file.ReadAllLines().Select(line => CreateTask(line)).OrderByDescending(task=>task.Date).First();
        }

        private static Task CreateTask(string line)
        {
            string[] record = line.Split(',');

            return new Task(
                DateTime.Parse(record[0]),
                record[2],
                TrimQuotes(record[3]),
                TrimQuotes(record[4]));
        }

        private static string TrimQuotes(string value)
        {
            char[] doubleQuote = new[] {'"'};

            return value.TrimStart(doubleQuote).TrimEnd(doubleQuote);
        }
    }
}