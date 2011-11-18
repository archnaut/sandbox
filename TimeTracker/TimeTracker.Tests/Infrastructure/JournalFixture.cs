using System;
using NUnit.Framework;
using TimeTracker.Infrastructure;
using System.Data.EntityClient;

namespace TimeTracker.Tests.Infrastructure
{
	[TestFixture]
	public class JournalFixture
	{
		[Test]
		public void EntitiesTest()
		{
			var systemUnderTest = new Journal();
		}
	}
}
