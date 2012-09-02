using System;
using CompositeClient.TimeTracking.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace CompositeClient.TimeTracking
{
	public class TimeTrackingModule : IModule
	{
        private readonly IRegionViewRegistry regionViewRegistry;
        
		public TimeTrackingModule(IRegionViewRegistry registry)
		{
			this.regionViewRegistry = registry;   
		}

        public void Initialize()
        {
            regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(TimeTrackingView));
        }
	}
}
