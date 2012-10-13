using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracking.DomainLayer;
using Raven.Client;

namespace DataAccess.Raven
{
    internal class Repository : IRepository
    {
    	private IDocumentStore _documentStore;
        private IRavenUnitOfWork _unitOfWork;
        
        public Repository(IDocumentStore documentStore)
        {
        	_documentStore = documentStore;
        }

        public System.Linq.IQueryable<T> AllInstances<T>() where T : class
		{
			return _unitOfWork.Query<T>();
		}
    	
        public void Add<T>(T item) where T : class
		{
			_unitOfWork.Store(item);
		}
    	
        public void Remove<T>(T item) where T : class
		{
			_unitOfWork.Delete(item);
		}
        
        public IUnitOfWork NewUnitOfWork()
        {
        	return _unitOfWork = new UnitOfWork(OpenSession());
        }
        
        private IDocumentSession OpenSession()
        {
            var session = _documentStore.OpenSession();
            session.Advanced.UseOptimisticConcurrency = true;
            return session;
        }
    }
}