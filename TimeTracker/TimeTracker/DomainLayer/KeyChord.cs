using System;
using System.Collections.Generic;
using System.Linq;
using UserActivity;
using System.Threading.Tasks;

namespace TimeTracker.ApplicationLayer
{
	public interface IKeyChord{
		event EventHandler Struck;
	}
	
	public class KeyChord : IKeyChord
	{	
		private List<VirtualKeyCode> _pressedKeys;
		private IKeyboard _keyboard;
		private IEnumerable<VirtualKeyCode> _expectedKeys;
		
		public KeyChord(IKeyboard keyboard, IAppSettings settings)
		{
			_pressedKeys = new List<VirtualKeyCode>();
			_keyboard = keyboard;
			
			if(settings.Chord
			
		}
		
		public event EventHandler Struck = delegate{};
		
		private void OnKeyUp(object sender, KeyboardEventArgs args){
			if(_pressedKeys.OrderBy(x=>x).SequenceEqual(_expectedKeys)){
				Task.Factory.StartNew(()=>Struck(this, EventArgs.Empty));
				args.Handled = true;
			}
		}
		
		private void OnKeyDown(object sender, KeyboardEventArgs args){
			_pressedKeys.Add(_keyboard.KeyPressed);
			args.Handled = true;
		}
	}
}
