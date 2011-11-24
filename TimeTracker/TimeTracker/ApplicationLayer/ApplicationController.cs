using System;
using TimeTracker.DomainLayer;
using UserActivity;

namespace TimeTracker.ApplicationLayer
{
    public class ApplicationController
    {
        private readonly IApplication _application;
		private readonly IPresentationController _presentationController;
        private readonly IKeyboard _keyboard;
        private readonly IHotKeySpecification _hotKeySpecification;

        public ApplicationController(IPresentationController presentationContoller, IKeyboard keyboard)
            :this(presentationContoller, keyboard, new ApplicationAdapter(), new HotKeySpecification()){}

        public ApplicationController(
			IPresentationController presentationController			
            , IKeyboard keyboard
            , IApplication application
            , IHotKeySpecification hotKeySpecification)
        {
 			_presentationController = presentationController;
            _keyboard = keyboard;
            _application = application;
            _hotKeySpecification = hotKeySpecification;

 			_presentationController.ExitApplication += ExitApplication;
            _keyboard.KeyDown += OnKeyboardKeyDown;
        }

        private void OnKeyboardKeyDown(object sender, KeyboardEventArgs args)
        {
            if (!_hotKeySpecification.IsSatisfiedBy(_keyboard)) 
                return;
            
            _presentationController.ShowEntryView();
            args.Handled = true;
        }

        private void ExitApplication(object sender, EventArgs args)
        {
            _keyboard.KeyDown -= OnKeyboardKeyDown;
            
            _presentationController.ExitApplication -= ExitApplication;
            
            _application.Exit();
        }
    }
}