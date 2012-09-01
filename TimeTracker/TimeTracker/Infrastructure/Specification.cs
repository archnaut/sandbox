using System;

namespace TimeTracking.Infrastructure
{
	public abstract class Specification<TCandidate>
	{
		public abstract bool IsSatisfiedBy(TCandidate candidate);
		
		public Specification<TCandidate> OR(Specification<TCandidate> specification)
		{
			return new OrSpecification(this, specification);
		}
		
		public Specification<TCandidate> And(Specification<TCandidate> specification)
		{
			return new AndSpecification(this, specification);
		}
		
		private class OrSpecification : Specification<TCandidate>
		{
			private Specification<TCandidate> _left;
			private Specification<TCandidate> _right;
			
			public OrSpecification(Specification<TCandidate> left, Specification<TCandidate> right){
				_left = left;
				_right = right;
			}
			
			public override bool IsSatisfiedBy(TCandidate candidate)
			{
				return _left.IsSatisfiedBy(candidate) || _right.IsSatisfiedBy(candidate);
			}
		}
		
		private class AndSpecification : Specification<TCandidate>
		{
			private Specification<TCandidate> _left;
			private Specification<TCandidate> _right;
			
			public AndSpecification(Specification<TCandidate> left, Specification<TCandidate> right){
				_left = left;
				_right = right;
			}
			
			public override bool IsSatisfiedBy(TCandidate candidate)
			{
				return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
			}
		}

		private class NotSpecification : Specification<TCandidate>{
			private Specification<TCandidate> _specification;
			
			public NotSpecification(Specification<TCandidate> specification){
				_specification = specification;
			}
			
			public override bool IsSatisfiedBy(TCandidate candidate)
			{
				return !_specification.IsSatisfiedBy(candidate);
			}
		}
	}
}
