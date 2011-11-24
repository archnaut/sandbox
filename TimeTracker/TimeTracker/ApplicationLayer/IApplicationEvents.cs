using System;

namespace TimeTracker.ApplicationLayer
{
    public interface IApplicationEvents
    {
        event EventHandler ExitApplication;
    }
}