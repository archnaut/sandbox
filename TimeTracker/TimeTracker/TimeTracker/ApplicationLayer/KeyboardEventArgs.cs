using System;

namespace TimeTracker.ApplicationLayer
{
    public class KeyboardEventArgs : EventArgs
    {
        public bool Handled { get; set; }
    }
}