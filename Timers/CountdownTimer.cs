using System;

namespace PolyWare.Timers {
	public class CountdownTimer : Timer {
		public Action OnTimerComplete = delegate { };
		
		public CountdownTimer(float value) : base(value) { }
		
		public override void Tick(float deltaTime) {
			base.Tick(deltaTime);
			
			if (IsRunning && Time > 0) {
				Time -= deltaTime;
			}

			if (IsRunning && IsFinished) {
				Complete();
			}
		}

		private void Complete() {
			if (!IsRunning) return;
			IsRunning = false;
			OnTimerComplete?.Invoke();
		}
		
		protected virtual bool IsFinished => Time <= 0;
	}

	/// <summary>
	/// countdown timer that when time runs out, it will check a predicate before it stops running
	/// </summary>
	public class CountdownWithPredicateTimer : CountdownTimer {
		private readonly Func<bool> completionCondition;

		public CountdownWithPredicateTimer(float value, Func<bool> predicate) : base(value) {
			completionCondition = predicate;
		}

		protected override bool IsFinished => Time <= 0 && completionCondition();
	}
}