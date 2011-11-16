namespace TimeTracker.DomainLayer
{
    public interface ITimesheet
    {
        void Update(Task task);
        Task GetLastTask();
    }
}