using System;
using System.Configuration;
using UserActivity;

namespace TimeTracker.ApplicationLayer
{
    public interface IHotKeySpecification
    {
        bool IsSatisfiedBy(IKeyboard keyboard);
    }

    public class HotKeySpecification : IHotKeySpecification
    {
        private static readonly bool Shift = bool.Parse(ConfigurationManager.AppSettings["shift"]);
        private static readonly bool Alt = bool.Parse(ConfigurationManager.AppSettings["alt"]);
        private static readonly bool Ctrl = bool.Parse(ConfigurationManager.AppSettings["ctrl"]);
        private static readonly VirtualKeyCode Key = 
            (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + ConfigurationManager.AppSettings["key"].ToUpper());

        public bool IsSatisfiedBy(IKeyboard keyboard)
        {
            if(keyboard.KeyPressed != Key)
                return false;

            bool satisfied = true;

            satisfied &= keyboard.AltPressed == Alt;
            satisfied &= keyboard.CtrlPressed == Ctrl;
            satisfied &= keyboard.ShiftPressed == Shift;

            return satisfied;
        }
    }
}