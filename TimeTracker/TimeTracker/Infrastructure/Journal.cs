using System.Data.Entity;
using TimeTracking.DomainLayer;

namespace TimeTracking.Infrastructure
{
	public class Journal : DbContext
	{
		public DbSet<Entry> Entries{get; set;}
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.ComplexType<Duration>();
		}
		
		static Journal()
		{
			Database.SetInitializer<Journal>(new DropCreateDatabaseAlways<Journal>());
		}
	}
}
