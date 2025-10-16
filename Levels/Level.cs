using System;
using System.Collections;
using PolyWare.Analytics.Telemetry;
using PolyWare.Cameras;
using PolyWare.Core.Services;
using PolyWare.Timers;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Levels {
	public abstract class Level : MonoBehaviour {
		[SerializeField] private string levelName;
		[SerializeField] private Transform playerStart;
		public Transform PlayerStart => playerStart;

		public static event Action<Level> OnLevelLoaded = delegate { };
		public static event Action OnLevelStarted = delegate { };
		public static event Action OnLevelReset = delegate { };
		public static event Action OnLevelUnloaded = delegate { };
		public static event Action OnLevelComplete = delegate { };

		public bool LevelStarted { get; private set; }
		public bool LevelEnded { get; private set; }
		
		private Stopwatch levelTimer = new Stopwatch();
		
		protected abstract void OnLoadLevel();
		public abstract void OnUnLoadLevel();

		protected abstract void OnStartLevel();
		protected abstract void OnLevelUpdate();
		protected abstract void OnCompleteLevel();
		protected abstract void OnResetLevel();

		private void Awake() {
			StartCoroutine(SlowAwake());
		}

		IEnumerator SlowAwake() {
			// levels can't start until there is a game service. Wait until there is one.
			// this happens when the level is loaded quicker than the core scenes, or you are
			// editing a level in the editor and press play without the core scenes loaded
			while (ServiceLocator.Global == null || !ServiceLocator.Global.Has<IGameService>() || !ServiceLocator.Global.Has<ICameraService>() || !ServiceLocator.Global.Has<IUIService>()) {
				yield return Yielders.WaitForEndOfFrame;
			}
			
			OnLoadLevel();
			OnLevelLoaded?.Invoke(this);
			
			yield return Yielders.WaitForEndOfFrame; // wait for children awake calls
			
			#if UNITY_EDITOR
			StartLevel();
			#endif
		}

		public void StartLevel() {
			ServiceLocator.Global.Get<ITelemetryService>().LogEvent(new LevelTelemetryEvents.LevelStart(levelName));

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

			ServiceLocator.Global.Get<ITelemetryService>().LogEvent(new LevelTelemetryEvents.LevelComplete(levelName, TimeFormatter.GetFormattedTime(levelTimer)));
			
			LevelEnded = true;
			OnLevelComplete?.Invoke();
			
			OnCompleteLevel();
		}

		public void RestartLevel() {
			LevelStarted = false;
			LevelEnded = false;
			
			levelTimer.Stop();
			
			ServiceLocator.Global.Get<ITelemetryService>().LogEvent(new LevelTelemetryEvents.LevelReset(levelName, TimeFormatter.GetFormattedTime(levelTimer)));
			
			OnResetLevel();
			
			OnLevelReset.Invoke();
		}

		private void OnDestroy() {
			OnLevelUnloaded?.Invoke();
		}
	}
}