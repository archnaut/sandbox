using System;
using System.Configuration;
using System.Windows.Forms;
using TimeTracker.ApplicationLayer;
using TimeTracker.DomainLayer;
using TimeTracker.Infrastructure;
using TimeTracker.PresentationLayer;
using TimeTracker.PresentationLayer.ViewLayer;

namespace TimeTracker
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Timesheet timesheet = new Timesheet(ConfigurationManager.AppSettings["Timesheet"]);
            RecentActivities recentActivities = new RecentActivities(new FileAdapter(ConfigurationManager.AppSettings["RecentActivitiesFile"]));

            using (TaskEntryForm taskEntryForm = new TaskEntryForm())
            using (NotifyIconView notifyIconView = new NotifyIconView())
            {
                TaskEntryPresenter presenter = new TaskEntryPresenter(taskEntryForm, timesheet, recentActivities);
                NotifyIconPresenter iconPresenter = new NotifyIconPresenter(notifyIconView);
                new ApplicationController(presenter, iconPresenter);

                Application.Run();
            }
        }
    }
}