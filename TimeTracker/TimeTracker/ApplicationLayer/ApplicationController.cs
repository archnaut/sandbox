using System;
using TimeTracker.DomainLayer;
using UserActivity;

namespace TimeTracker.ApplicationLayer
{
    public class ApplicationController
    {
        private readonly IApplication _application;
        private readonly ITaskEntryPresenter _presenter;
        private readonly IApplicationExit _applicationExit;
        private readonly IKeyboard _keyboard;
        private readonly IHotKeySpecification _hotKeySpecification;

        public ApplicationController(ITaskEntryPresenter presenter, IApplicationExit applicationExit)
            :this(presenter, applicationExit, new Keyboard(), new ApplicationAdapter(), new HotKeySpecification()){}

        public ApplicationController(
            ITaskEntryPresenter presenter, IApplicationExit applicationExit, IKeyboard keyboard, IApplication application, IHotKeySpecification hotKeySpecification) 
        {
            _applicationExit = applicationExit;
            _applicationExit.ExitApplication += ExitApplication;

            _keyboard = keyboard;
            _keyboard.KeyDown += OnKeyboardKeyDown;

            _presenter = presenter;
            _application = application;

            _hotKeySpecification = hotKeySpecification;
        }

        private void OnKeyboardKeyDown(object sender, KeyboardEventArgs args)
        {
            if (!_hotKeySpecification.IsSatisfiedBy(_keyboard)) 
                return;
            
            _presenter.ShowView();
            args.Handled = true;
        }

        private void ExitApplication(object sender, EventArgs args)
        {
            _keyboard.KeyDown -= OnKeyboardKeyDown;
            
            _applicationExit.ExitApplication -= ExitApplication;
            
            _application.Exit();
        }
    }
}