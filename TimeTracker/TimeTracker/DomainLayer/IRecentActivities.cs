using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracking.DomainLayer
{
	public interface IRecentActivities
	{
		string First{get;}
		void Add(string activity);
		string[] ToArray();
	}
}
