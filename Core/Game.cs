using PolyWare.AssetManagement.Registries;
using UnityEngine;

namespace PolyWare {
	public abstract class Game : MonoBehaviour {
		
		[SerializeField] protected LevelRegistry levelRegistry;

		public void Initialize() {
			levelRegistry.Initialize();
			
			OnInitialize();
		}

		protected abstract void OnInitialize();
		public abstract void LoadGame();
		public abstract void StartGame();
	}
}