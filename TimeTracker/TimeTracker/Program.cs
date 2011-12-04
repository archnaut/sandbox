using System;
using System.Configuration;
using System.Data.Entity;
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
        	Database.SetInitializer<Journal>(new DropCreateDatabaseAlways<Journal>());
        	
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