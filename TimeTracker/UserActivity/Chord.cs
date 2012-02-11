using System;
using System.Collections.Generic;

namespace UserActivity
{
	public struct Chord
	{
		private VirtualKeyCode _actionKey;
		private IDictionary<VirtualKeyCode, KeyState> _modifierKeyState;
		
		public Chord(VirtualKeyCode actionKey, IDictionary<VirtualKeyCode, KeyState> modifierKeyState)
		{
			_actionKey = actionKey;
			_modifierKeyState = modifierKeyState;
		}
		
		public VirtualKeyCode ActionKey
		{
			get{return _actionKey;}
		}
		
		public KeyState AltKeyState
		{
			get{return _modifierKeyState[VirtualKeyCode.VK_MENU];}
		}
		
		public KeyState CtrlKeyState
		{
			get{return _modifierKeyState[VirtualKeyCode.VK_CONTROL];}
		}
		
		public KeyState ShiftKeyState
		{
			get{return _modifierKeyState[VirtualKeyCode.VK_SHIFT];}
		}
	}
}
