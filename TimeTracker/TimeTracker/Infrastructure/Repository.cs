using System;
using System.Data.Entity;
using System.Linq;

using TimeTracker.DomainLayer;

namespace TimeTracker.Infrastructure
{
	public class Repository : IRepository
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

		public void Commit()
		{
			_context.SaveChanges();
		}
	}
}
