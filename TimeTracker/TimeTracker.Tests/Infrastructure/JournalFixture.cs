using System;
using System.Data.EntityClient;
using System.Linq;

using NUnit.Framework;
using TimeTracker.DomainLayer;
using TimeTracker.Infrastructure;

namespace TimeTracker.Tests.Infrastructure
{
	[TestFixture]
	public class JournalFixture
	{
		[Test]
		public void EntitiesTest()
		{
			using(var systemUnderTest = new Journal())
			{
				systemUnderTest.Entries.Add(new Entry(DateTime.Now, 100, "Test", "Note"));
				systemUnderTest.SaveChanges();       
			}
			
			var journal = new Journal();
			var entries = journal.Entries;
			foreach (var entry in entries) {
				Console.WriteLine("EntryID: {0}", entry.EntryID);
				Console.WriteLine("Activity: {0}", entry.Activity);
				Console.WriteLine("Durattion: {0}", (TimeSpan)entry.Duration);
				Console.WriteLine("Date: {0}", entry.Date);
				Console.WriteLine("Note: {0}\n\n", entry.Note);
			}
		}
	}
}
