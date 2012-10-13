using System;
using System.Data.Entity;
using System.Linq;

using TimeTracking.DomainLayer;

namespace DataAccess.EF
{
	internal class Repository : IRepository
	{
		private DbContext _context;

		public Repository(DbContext context)
		{
			_context = context;
		}

		public IQueryable<T> AllInstances<T>() where T : class
		{
			return _context.Set<T>();
		}

		public T Get<T>() where T : class
		{
			return _context.Set<T>().FirstOrDefault();
		}

		public void Add<T>(T item) where T : class
		{
			_context.Set<T>().Add(item);
		}

		public void Remove<T>(T item) where T : class
		{
			_context.Set<T>().Remove(item);
		}
		
		public IUnitOfWork NewUnitOfWork()
		{
			return new UnitOfWork(_context);
		}
		
		private class UnitOfWork : IUnitOfWork
		{
			DbContext _context;
			
			public UnitOfWork(DbContext context)
			{
				_context = context;
			}
			
			public void Commit()
			{
				_context.SaveChanges();
			}
			
			public void Dispose()
			{
			}
		}

	}
}
