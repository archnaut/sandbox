using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using TimeTracking.DomainLayer;

namespace DataAccess.Raven
{
    internal interface IRavenUnitOfWork : IUnitOfWork
    {
    	
		System.Linq.IQueryable<T> Query<T>();
    	
		void Store<T>(T item);
    	
		void Delete<T>(T item);
    }

    internal class UnitOfWork : IRavenUnitOfWork
    {
        private IDocumentSession _session;
        
        public UnitOfWork(IDocumentSession session)
        {
            _session = session;
        }

        public void Commit()
        {
        	if(_session == null) return;
        	
            _session.SaveChanges();
        }
    	
		public System.Linq.IQueryable<T> Query<T>()
		{
			if(_session == null) return new List<T>().AsQueryable();
			
			return _session.Query<T>();
		}
    	
		public void Store<T>(T item)
		{
			if(_session == null) return;
			
			_session.Store(item);
		}
    	
		public void Delete<T>(T item)
		{
			if(_session == null) return;
			
			_session.Delete(item);
		}
    	
		public void Dispose()
		{
			if(_session == null) return;
			
			_session.Dispose();
			_session = null;
		}
    }
}
