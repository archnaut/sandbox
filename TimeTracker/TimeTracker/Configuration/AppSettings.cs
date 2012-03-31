using System;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using UserActivity;
using TimeTracker.ApplicationLayer;

namespace TimeTracker.Configuration
{
	public class AppSettings : IAppSettings
	{	
		private ChordSpecification _taskChordSpecification;
		
		public AppSettings()
		{
			_taskChordSpecification = GetTaskChordSpecification();
		}
			
		public ChordSpecification TaskChordSpecification
		{
			get{return _taskChordSpecification;}
		}
		
		private ChordSpecification GetTaskChordSpecification()
		{		
			var modifierKeyState = new Dictionary<VirtualKeyCode, KeyState>(){
				{VirtualKeyCode.VK_MENU, GetModifierKeyState(VirtualKeyCode.VK_MENU)},
				{VirtualKeyCode.VK_CONTROL, GetModifierKeyState(VirtualKeyCode.VK_CONTROL)},
				{VirtualKeyCode.VK_SHIFT, GetModifierKeyState(VirtualKeyCode.VK_SHIFT)}
			};

			return new ChordSpecification(GetActionKey(), modifierKeyState);
			
		}
		
		private KeyState GetModifierKeyState(VirtualKeyCode key)
		{
			switch (key) {
				case VirtualKeyCode.VK_MENU:
					return bool.Parse(ConfigurationManager.AppSettings["alt"]) ? KeyState.Down : KeyState.Up;
				case VirtualKeyCode.VK_CONTROL:
					return bool.Parse(ConfigurationManager.AppSettings["ctrl"]) ? KeyState.Down : KeyState.Up;
				case VirtualKeyCode.VK_SHIFT:
					return bool.Parse(ConfigurationManager.AppSettings["shift"]) ? KeyState.Down : KeyState.Up;
				default:
					throw new ArgumentException("Unknown VirtualKeyCode " + key);
			}
		}
		
		private VirtualKeyCode GetActionKey()
		{
			return (VirtualKeyCode)Enum.Parse(typeof(Keys), ConfigurationManager.AppSettings["actionKey"], true);
		}
	}
}
