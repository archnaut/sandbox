using System;
using System.Diagnostics;
using TimeTracking.DomainLayer;
using UserActivity;

namespace TimeTracking
{
	public class TimeTracker : IDisposable
    {
        private readonly IApplication _application;
		private readonly IPresentationController _presentationController;
		private readonly ChordSpecification _taskChordSpecification;

        public TimeTracker(
			IPresentationController presentationController			
            , IKeyboard keyboard
            , IApplication application
           	, ChordSpecification chordSpecification)
        {
 			_presentationController = presentationController;
            _application = application;
            
            keyboard.KeyUp += OnKeyUp;

 			_presentationController.ExitApplication += ExitApplication;
 			
 			_taskChordSpecification = chordSpecification;
        }
        
		public void Run()
		{
			_application.Run();
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