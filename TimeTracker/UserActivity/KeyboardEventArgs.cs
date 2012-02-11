using System;
using System.Collections.Generic;
using System.Linq;

namespace UserActivity
{
    public class KeyboardEventArgs : EventArgs
    {
  		private Chord _chord;
    	
    	public KeyboardEventArgs(int actionKey, IDictionary<VirtualKeyCode, KeyState> modifierKeyState)
    	{
    		_chord = new Chord((VirtualKeyCode)actionKey, modifierKeyState);
    	}
    	
		public Chord Chord
		{
			get{return _chord;}
		}
    	
        public bool Handled { get; set; }
    }
}