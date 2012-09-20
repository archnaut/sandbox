using System;
using System.Windows;
using CompositeClient.Views;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using TimeTracking;

namespace CompositeClient
{
	public class Program
	{
		[STAThread]
		public static void Main()
		{
			ClientBootStrapper.Bootstrap();
			ServiceLocator.Current.GetInstance<TimeTracker>().Run();
		}
	}
}
