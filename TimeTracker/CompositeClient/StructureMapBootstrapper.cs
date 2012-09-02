using System;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace CompositeClient
{
	public abstract class StructureMapBootstrapper : Bootstrapper
	{
		private bool useDefaultConfiguration;
		private IContainer _container;
		
		public StructureMapBootstrapper():this(new Container())
		{
		}
		
		public StructureMapBootstrapper(IContainer container)
		{
			_container = container;
		}
		
		public override void Run(bool runWithDefaultConfiguration)
		{
			useDefaultConfiguration = runWithDefaultConfiguration;

            Logger = CreateLogger();
            ModuleCatalog = CreateModuleCatalog();
            
            ConfigureModuleCatalog();

            ConfigureContainer();

            ConfigureServiceLocator();

            ConfigureRegionAdapterMappings();

            ConfigureDefaultRegionBehaviors();

            RegisterFrameworkExceptionTypes();

            Shell = CreateShell();

            if (Shell != null)
            {
                RegionManager.SetRegionManager(Shell, _container.GetInstance<IRegionManager>());
                RegionManager.UpdateRegions();
                InitializeShell();
            }
            
			InitializeModules();
		}
		
		private void ConfigureContainer()
		{
			_container.Configure(x=>x.For<IModuleCatalog>().Singleton().Use(ModuleCatalog));
            
			if (useDefaultConfiguration)
            {
				_container.Configure(x=>{
				                     	x.For<ILoggerFacade>().Use<TraceLogger>();
				                     	x.For<IModuleInitializer>().Singleton().Use<ModuleInitializer>();
				                     	x.For<IModuleManager>().Singleton().Use<ModuleManager>();
				                     	x.For<RegionAdapterMappings>().Singleton().Use<RegionAdapterMappings>();
                						x.For<IRegionManager>().Singleton().Use<RegionManager>();
                						x.For<IEventAggregator>().Singleton().Use<EventAggregator>();
                						x.For<IRegionViewRegistry>().Singleton().Use<RegionViewRegistry>();
                						x.For<IRegionBehaviorFactory>().Singleton().Use<RegionBehaviorFactory>();
                						x.For<IRegionNavigationJournalEntry>().Use<RegionNavigationJournalEntry>();
                						x.For<IRegionNavigationJournal>().Use<RegionNavigationJournal>();
                						x.For<IRegionNavigationService>().Use<RegionNavigationService>();
                						x.For<IRegionNavigationContentLoader>().Singleton().Use<RegionNavigationContentLoader>();
				                     });
            }

		}
		
		protected override void ConfigureServiceLocator()
		{
			ServiceLocator.SetLocatorProvider(()=>new StructureMapServiceLocator(_container));
		}
	}
}
