using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace UserActivity
{
    public class Keyboard : IKeyboard
    {
        /// <summary>
        /// This field is not objectively needed but we need to keep a reference on a delegate which will be 
        /// passed to unmanaged code. To avoid GC to clean it up.
        /// When passing delegates to unmanaged code, they must be kept alive by the managed application 
        /// until it is guaranteed that they will never be called.
        /// </summary>
        private static HookProc _keyboardCallback;
        private VirtualKeyCode _keyPressed;
        private EventHandler<KeyboardEventArgs> _onKeyUp;
        private EventHandler<KeyboardEventArgs> _onKeyDown;
        private readonly IUser32 _user32;

        public Keyboard()
        {
            _user32 = new User32Api();
            _keyboardCallback = KeyboardCallback;
        }

        internal Keyboard(IUser32 user32)
        {
            _user32 = user32;
            _keyboardCallback = KeyboardCallback;
        }

        public event EventHandler<KeyboardEventArgs> KeyUp
        {
            add
            {
                if (NoSubscribers())
                    HookKeyboardEvents();

                _onKeyUp += value;
            }
            remove
            {
                _onKeyUp -= value;

                if (!NoSubscribers())
                    UnhookKeyboardEvents();
            }
        }

        public event EventHandler<KeyboardEventArgs> KeyDown
        {
            add
            {
                if (NoSubscribers())
                    HookKeyboardEvents();

                _onKeyDown += value;
            }
            remove
            {
                _onKeyDown -= value;

                if (!NoSubscribers())
                    UnhookKeyboardEvents();
            }
        }

        public VirtualKeyCode KeyPressed
        {
            get { return _keyPressed; }
        }

        public bool CtrlPressed
        {
            get { return IsKeyPressed(VirtualKeyCode.VK_CONTROL); }
        }

        public bool AltPressed
        {
            get { return IsKeyPressed(VirtualKeyCode.VK_MENU); }
        }

        public bool ShiftPressed
        {
            get { return IsKeyPressed(VirtualKeyCode.VK_SHIFT); }
        }

        private int _keyboardHookHandle;

        private void HookKeyboardEvents()
        {
            //install hook
            IntPtr module = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);

            _keyboardHookHandle = _user32.SetWindowsHook(Constants.WH_KEYBOARD_LL, _keyboardCallback, module, 0);

            if (_keyboardHookHandle == 0)
                ThrowWin32Exception();
        }

        private void UnhookKeyboardEvents()
        {
            if (_keyboardHookHandle == 0)
                return;

            //uninstall hook
            int result = _user32.UnhookWindowsHook(_keyboardHookHandle);
            //reset invalid handle
            _keyboardHookHandle = 0;

            //Free up for GC
            _keyboardCallback = null;

            //if failed and exception must be thrown
            if (result == 0)
                ThrowWin32Exception();
        }

        private static void ThrowWin32Exception()
        {
            // Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
            // Initializes and throws a new instance of the Win32Exception class with the specified error. 
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private int KeyboardCallback(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode != Constants.HC_ACTION)
                return _user32.CallNextHook(_keyboardHookHandle, nCode, wParam, lParam);

            KeyboardEventArgs keyboardEventArgs = new KeyboardEventArgs();
            KeyboardHookStruct keyboardEventData = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

            switch (wParam)
            {
                case Constants.WM_KEYDOWN:
                case Constants.WM_SYSKEYDOWN:
                    _keyPressed = (VirtualKeyCode)keyboardEventData.VirtualKeyCode;
                    if(_onKeyDown != null) _onKeyDown(this, keyboardEventArgs);
                    break;
                case Constants.WM_KEYUP:
                case Constants.WM_SYSKEYUP:
                    _keyPressed = (VirtualKeyCode)keyboardEventData.VirtualKeyCode;
                    if(_onKeyUp != null) _onKeyUp(this, keyboardEventArgs);
                    break;
            }

            if (keyboardEventArgs.Handled)
                return -1;

            return _user32.CallNextHook(_keyboardHookHandle, nCode, wParam, lParam);
        }

        private bool IsKeyPressed(VirtualKeyCode key)
        {
            return (_user32.GetKeyState(key) & 0x8000) != 0;
        }

        private bool NoSubscribers()
        {
            return _onKeyUp == null && _onKeyDown == null;
        }
    }
}