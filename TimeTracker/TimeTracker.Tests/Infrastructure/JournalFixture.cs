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
				systemUnderTest.Entries.Add(new Entry(DateTime.Now, "100", "Test", "Note"));
				systemUnderTest.SaveChanges();       
			}
			
			var journal = new Journal();
			var first = journal.Entries.FirstOrDefault();
			
			Console.WriteLine("EntryID: {0}", first.EntryID);
			Console.WriteLine("Activity: {0}", first.Activity);
			Console.WriteLine("Date: {0}", first.Date);
			Console.WriteLine("Note: {0}", first.Note);
		}
	}
}
