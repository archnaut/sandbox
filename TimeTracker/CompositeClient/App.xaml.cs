using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace CompositeClient
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ClientBootStrapper.Bootstrap();
		}
	}
}