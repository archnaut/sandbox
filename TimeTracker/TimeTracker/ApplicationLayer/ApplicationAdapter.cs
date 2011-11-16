using System.Windows.Forms;

namespace TimeTracker.ApplicationLayer
{
    public interface IApplication
    {
        void Exit();
    }

    public class ApplicationAdapter : IApplication
    {
        public void Exit()
        {
            Application.Exit();
        }
    }
}