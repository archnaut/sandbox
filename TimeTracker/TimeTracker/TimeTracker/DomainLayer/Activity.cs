namespace TimeTracker.DomainLayer
{
    public struct Activity
    {
        private readonly string _activityName;
        private readonly long _timestamp;

        public Activity(string activityName, long timestamp)
        {
            _timestamp = timestamp;
            _activityName = activityName;
        }

        public long Timestamp
        {
            get { return _timestamp; }
        }

        public string ActivityName
        {
            get { return _activityName; }
        }

        public override string ToString()
        {
            return _activityName;
        }
    }
}