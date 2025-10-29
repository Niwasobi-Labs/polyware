using System;

namespace PolyWare.Timers {
	public abstract class Timer {
		protected float initialTime;
		protected float Time { get; set; }
		
		public bool IsRunning { get; protected set; }

		public Action OnTimerStart = delegate { };
		public Action OnTimerTick = delegate { };
		public Action OnTimerStop = delegate { };

		public float Progress => Time / initialTime;
		
		protected Timer(float value) {
			initialTime = value;
			IsRunning = false;
		}

		public void Start() {
			Time = initialTime;
			if (IsRunning) return;
			
			IsRunning = true;
			OnTimerStart?.Invoke();
		}

		public void StartAt(float progress) {
			Start();
			Time = initialTime * progress;
		}
		
		/// <summary>
		/// call base.Tick in order to fire OnTimerTick event
		/// </summary>
		/// <param name="deltaTime"></param>
		public virtual void Tick(float deltaTime) {
			if (IsRunning) OnTimerTick?.Invoke();
		}

		public void SetInitialTime(float newInitialTime) {
			initialTime = newInitialTime;
			IsRunning = false;
		}
		
		public void Stop() {
			if (!IsRunning) return;

			IsRunning = false;
			OnTimerStop?.Invoke();
		}

		public void Restart() {
			Stop();
			Start();
		}
		
		public void Pause() => IsRunning = false;
		public void Resume() => IsRunning = true;
		public float GetTime => Time;
	}
}