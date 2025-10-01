using System;

namespace PolyWare.Timers {
	public class IntervalTimer : CountdownTimer {
		public IntervalTimer(float time, float interval) : base(time) {
			this.interval = interval;
			elaspedTime = 0;
		}
		
		public event Action OnInterval;

		private float interval;
		private float elaspedTime;
		
		public override void Tick(float deltaTime) {
			base.Tick(deltaTime);
			
			elaspedTime += deltaTime;

			if (elaspedTime < interval) return;
			
			OnInterval?.Invoke();
			elaspedTime = 0;
		}
	}
}