using System;
using TimeTracker.ApplicationLayer;
using UserActivity;
using KeyboardEventArgs=TimeTracker.ApplicationLayer.KeyboardEventArgs;

namespace TimeTracker.Infrastructure
{
    public class KeyboardAdapter : IKeyboard
    {
        private readonly Keyboard _keyboard = new Keyboard();
        
        public VirtualKeyCode KeyPressed
        {
            get { return _keyboard.KeyPressed; }
        }

        public bool CtrlPressed
        {
            get { return _keyboard.CtrlPressed; }
        }

        public bool AltPressed
        {
            get { return _keyboard.AltPressed; }
        }

        public bool ShiftPressed
        {
            get { return _keyboard.ShiftPressed; }
        }

        public event EventHandler<KeyboardEventArgs> KeyUp
        {
            add { _keyboard.KeyUp += value; }
            remove { _keyboard.KeyUp -= value; }
        }

        public event EventHandler<KeyboardEventArgs> KeyDown
        {
            add { _keyboard.KeyDown += value; }
            remove { _keyboard.KeyDown -= value; }
        }
    }
}