using System;
using System.Collections.Specialized;
using System.Configuration;

namespace TimeTracking
{
	public class TimeTrackerChord : ChordSpecification
	{
		public TimeTrackerChord()
		{
			NameValueCollection _appSettings = ConfigurationManager.AppSettings;
			
			AltKey(bool.Parse(_appSettings["alt"]));
			CtrlKey(bool.Parse(_appSettings["ctrl"]));
	        ShiftKey(bool.Parse(_appSettings["shift"]));
	        ActionKey(ConfigurationManager.AppSettings["actionKey"]);
		}
	}
}
