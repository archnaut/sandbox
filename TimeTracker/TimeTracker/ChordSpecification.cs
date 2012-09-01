using System;
using System.Collections.Generic;
using UserActivity;
using TimeTracking.Infrastructure;

namespace TimeTracking
{
	public abstract class ChordSpecification
	{
		private ChordBuilder _chordBulder = new ChordBuilder();
		
		public bool IsStatisfiedBy(Chord candidate)
		{
			var chord = _chordBulder.Build();
			
			bool isSatisfied = true;
			isSatisfied &= candidate.ActionKey == chord.ActionKey;
			isSatisfied &= candidate.AltKeyState == chord.AltKeyState;
			isSatisfied &= candidate.CtrlKeyState == chord.CtrlKeyState;
			isSatisfied &= candidate.ShiftKeyState == chord.ShiftKeyState;
			return isSatisfied;
		}
		
		protected void AltKey(bool isIncluded)
		{
			if(isIncluded)
				_chordBulder.Set(VirtualKeyCode.VK_MENU, KeyState.Down);
			else
				_chordBulder.Set(VirtualKeyCode.VK_MENU, KeyState.Up);
		}

		protected void CtrlKey(bool isIncluded)
		{
			if(isIncluded)
				_chordBulder.Set(VirtualKeyCode.VK_CONTROL, KeyState.Down);
			else
				_chordBulder.Set(VirtualKeyCode.VK_CONTROL, KeyState.Up);
		}
		
		protected void ShiftKey(bool isIncluded)
		{
			if(isIncluded)
				_chordBulder.Set(VirtualKeyCode.VK_SHIFT, KeyState.Down);
			else
				_chordBulder.Set(VirtualKeyCode.VK_SHIFT, KeyState.Up);
		}
		
		protected void ActionKey(string key)
		{
			_chordBulder.ActionKey((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_{0}".Compose(key), true));
		}
			
		private class ChordBuilder
		{
			private Dictionary<VirtualKeyCode, KeyState> _modifierKeyState = new Dictionary<VirtualKeyCode, KeyState>();
			private VirtualKeyCode _actionKey;
			
			public void Set(VirtualKeyCode key, KeyState keyState)
			{
				if(_modifierKeyState.ContainsKey(key))
					_modifierKeyState[key] = keyState;
				else
					_modifierKeyState.Add(key, keyState);
			}
			
			public void ActionKey(VirtualKeyCode key)
			{
				_actionKey = key;
			}
			
			public Chord Build()
			{
				return new Chord(_actionKey, _modifierKeyState);
			}
		}
	}
}
