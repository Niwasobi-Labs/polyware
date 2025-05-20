using PolyWare.Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Levels {
	public abstract class Level : MonoBehaviour {
		[SerializeField] private string levelName;
		[SerializeField] private Transform playerStart;
		public Transform PlayerStart => playerStart;

		public static UnityAction OnLevelStarted = delegate { };
		public static UnityAction OnLevelReset = delegate { };

		protected IResettable[] Resettables { get; private set; }

		public bool LevelStarted { get; private set; }

		private void Awake() {
			// todo this is a bad design 
			Resettables = GetComponentsInChildren<IResettable>();
		}

		public abstract void LoadLevel();
		public abstract void UnLoadLevel();

		protected abstract void OnStartLevel();
		protected abstract void OnCompleteLevel();
		protected abstract void OnResetLevel();

		public void StartLevel() {
			// Core.Telemetry.LogEvent(new LevelStart(levelName));

			OnStartLevel();

			OnLevelStarted.Invoke();

			LevelStarted = true;

			// PolyWare.Core.Input.ChangeToActionMap(InputManager.ActionMap.Player);
		}

		public void LevelComplete() {
			// PolyWare.Core.Telemetry.LogEvent(new LevelComplete(levelName));

			OnCompleteLevel();
		}

		public void RestartLevel() {
			LevelStarted = false;

			// todo re-enable telemetry for levels, (fix telemetry event ids)
			// PolyWare.Core.Telemetry.LogEvent(new LevelReset(levelName));

			foreach (IResettable resettable in Resettables) resettable.Reset();

			OnResetLevel();

			OnLevelReset.Invoke();

			StartLevel();
		}
	}
}