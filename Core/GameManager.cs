using PolyWare.AssetManagement.Registries;
using UnityEngine;

namespace PolyWare.Game {
	public abstract class GameManager : MonoBehaviour, IGameManager {
		
		[SerializeField] protected LevelRegistry levelRegistry;

		protected abstract void OnInitialize();
		public void Initialize() {
			levelRegistry.Initialize();
			
			OnInitialize();
		}

		public abstract void LoadGame();

		protected abstract void OnStartGame();
		public void StartGame() {
			Core.Telemetry.LogEvent(new GameStartTelemetryEvent());
			OnStartGame();
		}
	}
}