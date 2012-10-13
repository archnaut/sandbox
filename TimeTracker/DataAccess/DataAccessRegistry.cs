using System;
using System.Data.Entity;
using StructureMap;
using StructureMap.Configuration.DSL;
using TimeTracking.DomainLayer;

namespace DataAccess.EF
{
	public class DataAccessRegistry : Registry
	{
		public DataAccessRegistry()
		{
			Profile("SqlServerCompact", x=>{
				x.For<IRepository>().Use<Repository>();
				x.For<DbContext>().Use<Journal>();
			});
		}
	}
}
