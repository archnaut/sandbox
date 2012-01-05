using System;

namespace UserActivity
{
    public interface IKeyboard
    {
        event EventHandler<KeyboardEventArgs> KeyUp;
        event EventHandler<KeyboardEventArgs> KeyDown;

        VirtualKeyCode KeyPressed { get; }
    }
}