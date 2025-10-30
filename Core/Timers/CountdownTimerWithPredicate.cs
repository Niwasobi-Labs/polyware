using System;

namespace PolyWare.Core {
	/// <summary>
	/// countdown timer that when time runs out, it will check a predicate before it stops running
	/// </summary>
	public class CountdownTimerWithPredicate : CountdownTimer {
		private readonly Func<bool> completionCondition;

		public CountdownTimerWithPredicate(float value, Func<bool> predicate) : base(value) {
			completionCondition = predicate;
		}

		public override bool IsFinished => Time <= 0 && completionCondition();
	}
}