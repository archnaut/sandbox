using System.IO;
using TimeTracker.DomainLayer;

namespace TimeTracker.Infrastructure
{
    public class FileAdapter : IFile
    {
        private readonly string _fileName;

        public FileAdapter(string fileName)
        {
            _fileName = fileName;
        }

        public void AppendAllText(string content)
        {
            File.AppendAllText(_fileName, content);
        }

        public bool Exists()
        {
            return File.Exists(_fileName);
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines(_fileName);
        }

        public void WriteAllLines(string[] content)
        {
            File.WriteAllLines(_fileName, content);
        }
    }
}