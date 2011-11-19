using System.Data.Entity;
using TimeTracker.DomainLayer;

namespace TimeTracker.Infrastructure
{
	public class Journal : DbContext
	{
		public DbSet<Entry> Entries{get; set;}
	}
}
