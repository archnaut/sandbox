using System;
using System.Data.Entity;
using System.Linq;

namespace TimeTracking.DomainLayer
{
	public interface IRepository
	{
		IQueryable<T> AllInstances<T>() where T : class;
		void Add<T>(T item) where T : class;
		void Remove<T>(T item) where T : class;
		
		IUnitOfWork NewUnitOfWork();
	}
}
