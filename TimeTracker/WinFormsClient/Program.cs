using System;
using System.Configuration;
using System.Data.Entity;
using System.Windows.Forms;

using TimeTracking;
using TimeTracking.DomainLayer;
using TimeTracking.Infrastructure;
using TimeTracking.PresentationLayer;
using TimeTracking.PresentationLayer.ViewLayer;
using System.Diagnostics;

namespace TimeTracking
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
        	Debug.Print(typeof(Bootstrapper).AssemblyQualifiedName);
        	Bootstrapper.Bootstrap();
        	
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var timeTracker = Container.Get<TimeTracker>())
            {
                timeTracker.Run();
            }
        }
    }
}