using System;

namespace TimeTracker.ApplicationLayer
{
    public interface IApplicationExit
    {
        event EventHandler ExitApplication;
    }
}