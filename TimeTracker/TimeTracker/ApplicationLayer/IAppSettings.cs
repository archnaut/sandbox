using System;
using System.Windows.Forms;
using System.Windows.Input;
using System.Configuration;
using System.Collections.Generic;
using UserActivity;
using TimeTracker.Configuration;

namespace TimeTracker.ApplicationLayer
{
	public interface IAppSettings{
		ChordSpecification TaskChordSpecification{get;}
	}
}
