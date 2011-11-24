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

            var keyboard = Container.Get<IKeyboard>();
            var repository = Container.Get<IRepository>();

            using (var taskEntryForm = new TaskEntryForm())
            using (var notifyIconView = new NotifyIconView())
            {
                var entryPresenter = new TaskEntryPresenter(taskEntryForm, repository);
                var reportPresenter = new ReportPresenter();
                var presentationController = new PresentationController(entryPresenter, reportPresenter, notifyIconView);
                
                new ApplicationController(presentationController, keyboard);

                Application.Run();
            }
        }
    }
}