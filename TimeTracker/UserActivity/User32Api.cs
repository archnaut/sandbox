using System;
using System.Runtime.InteropServices;

namespace UserActivity
{
    internal delegate int HookProc(int nCode, int wParam, IntPtr lParam);

    internal interface IUser32
    {
        int SetWindowsHook(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);
        int UnhookWindowsHook(int idHook);
        int CallNextHook(int idHook, int nCode, int wParam, IntPtr lParam);
        short GetKeyState(VirtualKeyCode key);
    }

    internal class User32Api : IUser32
    {
        public int SetWindowsHook(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId)
        {
            return SetWindowsHookEx(idHook, lpfn, hMod, dwThreadId);
        }

        public int UnhookWindowsHook(int idHook)
        {
            return UnhookWindowsHookEx(idHook);
        }

        public int CallNextHook(int idHook, int nCode, int wParam, IntPtr lParam)
        {
            return CallNextHookEx(idHook, nCode, wParam, lParam);
        }

        public short GetKeyState(VirtualKeyCode key)
        {
            return GetAsyncKeyState((int)key);
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetAsyncKeyState(int vKey);
    }
}