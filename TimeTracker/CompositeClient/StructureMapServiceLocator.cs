using System;
using System.Diagnostics;
using System.Linq;

using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace CompositeClient
{
	public class StructureMapServiceLocator : ServiceLocatorImplBase
	{
		private IContainer _container;
			
		public StructureMapServiceLocator(IContainer container)
		{
			_container = container;
			
			_container.Configure(x=>x.For<IServiceLocator>().Singleton().Use(this));
		}
		
		protected override object DoGetInstance(Type serviceType, string key)
		{
			var msg = string.Format("{0} {1}",serviceType.Name, key);
			Debug.Print(msg);
			if(string.IsNullOrWhiteSpace(key))
				return _container.GetInstance(serviceType);
			
			return _container.GetInstance(serviceType, key);
		}
		
		protected override System.Collections.Generic.IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			var msg = string.Format("{0}",serviceType.Name);
			Debug.Print(msg);
			return _container.GetAllInstances(serviceType).Cast<object>();
		}
	}
}
