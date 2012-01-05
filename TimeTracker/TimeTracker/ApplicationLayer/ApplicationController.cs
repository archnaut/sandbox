using System;
using TimeTracker.DomainLayer;
using UserActivity;

namespace TimeTracker.ApplicationLayer
{
	public class ApplicationController : IDisposable
    {
        private readonly IApplication _application;
		private readonly IPresentationController _presentationController;

        public ApplicationController(IPresentationController presentationContoller, IKeyChord keyChord, IKeyboard keyboard)
            :this(presentationContoller, keyChord, new ApplicationAdapter()){}

        public ApplicationController(
			IPresentationController presentationController			
            , IKeyChord keyChord
            , IApplication application)
        {
 			_presentationController = presentationController;
            _application = application;
            
            keyChord.Struck += OnChordStruck;

 			_presentationController.ExitApplication += ExitApplication;
        }
        
        private void OnChordStruck(object sender, EventArgs args)
        {   
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