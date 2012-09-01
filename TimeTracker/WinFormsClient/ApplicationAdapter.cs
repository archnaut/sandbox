using System.Windows.Forms;

namespace TimeTracking
{
    public class ApplicationAdapter : IApplication
    {
    	public void Run()
    	{
    		Application.Run();
    	}
    	
        public void Exit()
        {
            Application.Exit();
        }
    }
}