﻿using System;
using System.Configuration;
using System.Windows.Forms;

using TimeTracker.ApplicationLayer;
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

            var timesheet = Container.Get<ITimesheet>();
            var recentActivities = Container.Get<IRecentActivities>();
            var keyboard = Container.Get<IKeyboard>();

            using (var taskEntryForm = new TaskEntryForm())
            using (var notifyIconView = new NotifyIconView())
            {
                TaskEntryPresenter presenter = new TaskEntryPresenter(taskEntryForm, timesheet, recentActivities);
                NotifyIconPresenter iconPresenter = new NotifyIconPresenter(notifyIconView);
                new ApplicationController(presenter, iconPresenter, keyboard);

                Application.Run();
            }
        }
    }
}