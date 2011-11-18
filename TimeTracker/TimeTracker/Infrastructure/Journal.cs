/*
 * Created by SharpDevelop.
 * User: Paul
 * Date: 11/17/2011
 * Time: 8:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.Entity;

namespace TimeTracker.Infrastructure
{
	/// <summary>
	/// Description of Journal.
	/// </summary>
	public class Journal : DbContext
	{
		public DbSet Entries{get; set;}
	}
}
