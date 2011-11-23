using System;
using System.Runtime.InteropServices;

namespace UserActivity
{
	internal interface IKernel32
	{
		IntPtr LoadDll(string name);
	}
	
	internal class Kernel32Api : IKernel32
	{
		public IntPtr LoadDll(string name)
		{
			return LoadLibrary(name);
		}
		
		[DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
	}
}
