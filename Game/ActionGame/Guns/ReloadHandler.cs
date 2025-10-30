using PolyWare.Core;
using UnityEngine.Events;

namespace PolyWare.Game {
	public abstract class ReloadHandler : IReloadHandler {
		protected readonly Gun gun;
		protected readonly CountdownTimer timer;
		
		public struct ReloadEventData {
			public enum State : byte {
				Started,
				Reloading,
				Cancelled,
				Completed
			}
		
			public float Progress;
			public State CurrentState;
		}

		private ReloadEventData reloadEventData;
		public event UnityAction<ReloadEventData> OnReload = delegate { };
		
		public bool IsReloading => timer.IsRunning;
		public abstract bool IsPreventingUse { get; }
		public abstract bool CanReload { get; }
		
		protected ReloadHandler(Gun gun) {
			this.gun = gun;

			timer = new CountdownTimer(gun.GunData.GunDefinition.ReloadTime);
			timer.OnTimerComplete += OnComplete;
		}

		protected void RaiseReloadEvent(float progress, ReloadEventData.State state) {
			reloadEventData.Progress = progress;
			reloadEventData.CurrentState = state;
			OnReload.Invoke(reloadEventData);
		}
		
		public void Start() {
			timer.Start();
			RaiseReloadEvent(0, ReloadEventData.State.Started);
		}

		public void Update(float deltaTime) {
			timer.Tick(deltaTime);
			if (timer.IsRunning) RaiseReloadEvent(1 - timer.Progress, ReloadEventData.State.Reloading);
		}

		public void Cancel() {
			timer.Stop();
			RaiseReloadEvent(1, ReloadEventData.State.Cancelled);
		}

		protected virtual void OnComplete() {
			RaiseReloadEvent(1, ReloadEventData.State.Completed);
		}
	}
}