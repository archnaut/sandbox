using System;
using System.Collections.Generic;
using UserActivity;

namespace TimeTracker.Configuration
{
	public class ChordSpecification
	{
		private Chord _chord;
		
		public ChordSpecification(VirtualKeyCode actionKey, IDictionary<VirtualKeyCode,KeyState> modifierKeyState)
		{
			_chord = new Chord(actionKey, modifierKeyState);
		}
		
		public bool IsStatisfiedBy(Chord chord)
		{
			bool isSatisfied = true;
			isSatisfied &= chord.ActionKey == _chord.ActionKey;
			isSatisfied &= chord.AltKeyState == _chord.AltKeyState;
			isSatisfied &= chord.CtrlKeyState == _chord.CtrlKeyState;
			isSatisfied &= chord.ShiftKeyState == _chord.ShiftKeyState;
			return isSatisfied;
		}
	}
}
