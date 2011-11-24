using System;
using System.Configuration;
using System.Windows.Forms;

using TimeTracker.ApplicationLayer;
using TimeTracker.Domain;
using TimeTracker.DomainLayer;
using TimeTracker.Infrastructure;
using TimeTracker.PresentationLayer;
using TimeTracker.PresentationLayer.ViewLayer;
using UserActivity;

namespace TimeTracker
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
        	Bootstrapper.Bootstrap();
        	
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var applicationController = Container.Get<ApplicationController>())
            {
                Application.Run();
            }
        }
    }
}