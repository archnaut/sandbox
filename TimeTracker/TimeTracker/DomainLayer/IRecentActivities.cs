using System.Collections.Generic;

namespace TimeTracker.DomainLayer
{
    public interface IRecentActivities : IEnumerable<string>
    {
        void Update(string taskName);
        string[] ToArray();
    }
}