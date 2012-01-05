using System;
using System.Configuration;
using System.Collections.Generic;
using UserActivity;

namespace TimeTracker
{
	public interface IAppSettings{
		VirtualKeyCode[] ExpectedKeys{get;}
	}
	public class AppSettings : IAppSettings
	{	
		public VirtualKeyCode[] ExpectedKeys{
			get{
				return GetExpectedKeys();
			}
		}
	
		private VirtualKeyCode[] GetExpectedKeys()
		{
			List<VirtualKeyCode> expectedKeys = new List<VirtualKeyCode>();
			
			if(bool.Parse(ConfigurationManager.AppSettings["shift"])) expectedKeys.Add(VirtualKeyCode.VK_SHIFT);
			if(bool.Parse(ConfigurationManager.AppSettings["alt"])) expectedKeys.Add(VirtualKeyCode.VK_MENU);
			if(bool.Parse(ConfigurationManager.AppSettings["ctrl"])) expectedKeys.Add(VirtualKeyCode.VK_CONTROL);
			
			expectedKeys.Add(
				(VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + ConfigurationManager.AppSettings["key"].ToUpper()));
			
			return expectedKeys.ToArray();
		}
	}
}
