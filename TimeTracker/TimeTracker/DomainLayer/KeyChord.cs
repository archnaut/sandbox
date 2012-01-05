using System;
using System.Collections.Generic;
using System.Linq;
using UserActivity;

namespace TimeTracker.ApplicationLayer
{
	public interface IKeyChord{
		event EventHandler Struck;
	}
	
	public class KeyChord : IKeyChord
	{	
		private List<VirtualKeyCode> _pressedKeys;
		private IKeyboard _keyboard;
		private IAppSettings _settings;
		
		public KeyChord(IKeyboard keyboard, IAppSettings settings)
		{
			_settings = settings;
			_pressedKeys = new List<VirtualKeyCode>();
			_keyboard = keyboard;
			
			_keyboard.KeyUp += OnKeyUp;
			_keyboard.KeyDown += OnKeyDown;
		}
		
		public event EventHandler Struck = delegate{};
		
		private void OnKeyUp(object sender, KeyboardEventArgs args){
			if(_pressedKeys.OrderBy(x=>x).SequenceEqual(_settings.ExpectedKeys.OrderBy(x=>x))){
				Struck(this, EventArgs.Empty);
				args.Handled = true;
			}
		}
		
		private void OnKeyDown(object sender, KeyboardEventArgs args){
			_pressedKeys.Add(_keyboard.KeyPressed);
			args.Handled = true;
		}
	}
}
