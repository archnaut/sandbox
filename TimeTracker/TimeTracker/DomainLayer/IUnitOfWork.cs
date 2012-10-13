using System;

namespace TimeTracking.DomainLayer
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
	}
}
