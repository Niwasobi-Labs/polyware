using PolyWare.Telemetry;
using PolyWare.Timers;
using PolyWare.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Levels {
	public abstract class Level : MonoBehaviour {
		[SerializeField] private string levelName;
		[SerializeField] private Transform playerStart;
		public Transform PlayerStart => playerStart;

		public static UnityAction OnLevelStarted = delegate { };
		public static UnityAction OnLevelReset = delegate { };

		public bool LevelStarted { get; private set; }
		
		private Stopwatch levelTimer = new Stopwatch();

		public abstract void LoadLevel();
		public abstract void UnLoadLevel();

		protected abstract void OnStartLevel();
		protected abstract void OnLevelUpdate();
		protected abstract void OnCompleteLevel();
		protected abstract void OnResetLevel();

		public void StartLevel() {
			Core.Telemetry.LogEvent(new LevelTelemetryEvents.LevelStart(levelName));

			OnStartLevel();

			OnLevelStarted.Invoke();

			levelTimer.Start();
			
			LevelStarted = true;
		}

		private void Update() {
			levelTimer.Tick(Time.deltaTime);
			
			OnLevelUpdate();
		}
		
		public void LevelComplete() {
			levelTimer.Stop();

			Core.Telemetry.LogEvent(new LevelTelemetryEvents.LevelComplete(levelName, TimeFormatter.GetFormattedTime(levelTimer)));
			
			OnCompleteLevel();
		}

		public void RestartLevel() {
			LevelStarted = false;
			
			levelTimer.Stop();
			
			Core.Telemetry.LogEvent(new LevelTelemetryEvents.LevelReset(levelName, TimeFormatter.GetFormattedTime(levelTimer)));

			OnResetLevel();

			OnLevelReset.Invoke();

			StartLevel();
		}
	}
}