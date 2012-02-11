using System;
using System.Diagnostics;
using TimeTracker.DomainLayer;
using UserActivity;
using TimeTracker.Configuration;

namespace TimeTracker.ApplicationLayer
{
	public class ApplicationController : IDisposable
    {
        private readonly IApplication _application;
		private readonly IPresentationController _presentationController;
		private readonly ChordSpecification _taskChordSpecification;

        public ApplicationController(IPresentationController presentationContoller, IKeyboard keyboard, IAppSettings settings)
            :this(presentationContoller, keyboard, new ApplicationAdapter(), settings){}

        public ApplicationController(
			IPresentationController presentationController			
            , IKeyboard keyboard
            , IApplication application
           	, IAppSettings settings)
        {
 			_presentationController = presentationController;
            _application = application;
            
            keyboard.KeyUp += OnKeyUp;

 			_presentationController.ExitApplication += ExitApplication;
 			
 			_taskChordSpecification = settings.TaskChordSpecification;
        }
        
        private void OnKeyUp(object sender, KeyboardEventArgs args)
        {   
        	if(_taskChordSpecification.IsStatisfiedBy(args.Chord))
            	_presentationController.ShowEntryView();
        }


        private void ExitApplication(object sender, EventArgs args)
        {   
            _presentationController.ExitApplication -= ExitApplication;
            
            _application.Exit();
        }
		
		public void Dispose()
		{
			_presentationController.Dispose();
		}
    }
}