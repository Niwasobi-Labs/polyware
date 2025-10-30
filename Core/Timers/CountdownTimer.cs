using System;

namespace PolyWare.Core {
	public class CountdownTimer : Timer {
		public event Action OnTimerComplete = delegate { };
		
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
		
		public virtual bool IsFinished => Time <= 0;
	}
}