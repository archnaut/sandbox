using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.DomainLayer
{
	public interface IRecentActivities
	{
		string First{get;}
		void Add(string activity);
		string[] ToArray();
	}
}
