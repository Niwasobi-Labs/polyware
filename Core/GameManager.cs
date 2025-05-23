using PolyWare.Analytics.Telemetry;
using PolyWare.Levels;
using UnityEngine;

namespace PolyWare.Core {
	public abstract class GameManager : MonoBehaviour, IGameManager {
		
		[SerializeField] protected LevelRegistry levelRegistry;
		public abstract Level CurrentLevel { get; }

		protected abstract void OnInitialize();
		public void Initialize() {
			levelRegistry.Initialize();
			
			OnInitialize();
		}

		public abstract void LoadGame();

		protected abstract void OnStartGame();
		public void StartGame() {
			Instance.Telemetry.LogEvent(new GameStartTelemetryEvent());
			OnStartGame();
		}
	}
}