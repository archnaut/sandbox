using System;
using System.Text;
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
        string GetKeyName(IntPtr lParam);
        bool KeyboardState(byte[] lpKeyState);
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
        
        public string GetKeyName(IntPtr lParam)
        {
        	var buffer = new StringBuilder(260);
        	var result = GetKeyNameTextW(lParam, buffer, 260);
        	return buffer.ToString();
        }
        
        public uint MapVirtualKeyToChar(uint uCode, uint uMapType)
        {
        	return MapVirtualKey(uCode, uMapType);
        }
        
        public bool KeyboardState(byte[] lpKeyState)
        {
        	return GetKeyboardState(lpKeyState);
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
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern int GetKeyNameTextW(IntPtr lParam, [Out] StringBuilder lpString, int nSize);
		
        [DllImport("user32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);
		
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetKeyboardState(byte [] lpKeyState);

    }
}