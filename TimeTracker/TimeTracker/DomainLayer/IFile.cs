namespace TimeTracker.DomainLayer
{
    public interface IFile
    {
        void AppendAllText(string content);
        bool Exists();
        string[] ReadAllLines();
        void WriteAllLines(string[] content);
    }
}