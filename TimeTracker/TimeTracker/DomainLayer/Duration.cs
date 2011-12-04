using System;

namespace TimeTracker.DomainLayer
{
	public class Duration
	{
		private const long TicksPerHour = 36000000000;
		private TimeSpan timeSpan;
		
		private Duration(){}
		
		public Duration(double hours)
		{
			var ticks = (long)(TicksPerHour * hours);
			timeSpan = new TimeSpan(ticks);
		}
		
		public long TickCount
		{
			get{ return timeSpan.Ticks; }
			private set{ timeSpan = new TimeSpan(value); }
		}
		
		public static implicit operator TimeSpan(Duration instance)
		{
			return instance.timeSpan;
		}
	}
}
