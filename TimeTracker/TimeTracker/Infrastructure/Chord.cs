using System;

namespace TimeTracker.Infrastructure
{
	public struct Chord : IEquatable<Chord>
	{
		int member;
		
		public override bool Equals(object obj)
		{
			if (obj is Chord)
				return Equals((Chord)obj);
			else
				return false;
		}
		
		public bool Equals(Chord other)
		{
			return this.member == other.member;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return member.GetHashCode();
		}
		
		public static bool operator ==(Chord left, Chord right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(Chord left, Chord right)
		{
			return !left.Equals(right);
		}
	
	}
}
