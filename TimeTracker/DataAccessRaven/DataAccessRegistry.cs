using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using TimeTracking.DomainLayer;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Listeners;
using Raven.Database.Server;
using Raven.Json.Linq;

namespace DataAccess.Raven
{
	public class DataAccessRegistry : Registry
	{
		public DataAccessRegistry()
		{
			Profile("RavenDB", x=>{
				x.For<IRepository>().Use<Repository>();
				x.For<IDocumentStore>().Use(()=>{
                        var store = new EmbeddableDocumentStore {DataDirectory = "./App_Data/Database"};

                        var etagHandler = new EntityMetadataHandler();
                        store.RegisterListener(etagHandler as IDocumentConversionListener);
                        store.RegisterListener(etagHandler as IDocumentStoreListener);

                        store.Initialize();
                        return store;
                });
			});
		}
		
		private class EntityMetadataHandler : IDocumentConversionListener, IDocumentStoreListener
        {
            private static readonly Type _typeOfEntity = typeof(AggregateRoot);

            public void EntityToDocument(object entity, RavenJObject document, RavenJObject metadata)
            {
            }

            public void DocumentToEntity(object entity, RavenJObject document, RavenJObject metadata)
            {
                UpdateMetadata(entity, metadata);
            }

            public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
            {
                return false;
            }

            public void AfterStore(string key, object entityInstance, RavenJObject metadata)
            {
                UpdateMetadata(entityInstance, metadata);
            }

            private void UpdateMetadata(object entity, RavenJObject metadata)
            {
                _typeOfEntity.GetProperty("Etag").SetValue(entity, metadata["@etag"].Value<Guid>(), null);
                _typeOfEntity.GetProperty("LastModified").SetValue(entity, metadata["Last-Modified"].Value<DateTime>(), null);
            }
        }
	}
}
