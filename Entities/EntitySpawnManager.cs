using UnityEngine;

namespace PolyWare.Entities {
	public abstract class EntitySpawnManager : MonoBehaviour {
		[SerializeField] private bool spawnOnStart = false;
		[SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
		[SerializeField] protected Transform[] spawnPoints;
		
		protected ISpawnPointStrategy spawnPointStrategy;

		protected enum SpawnPointStrategyType {
			Linear,
			Random
		}

		protected virtual void Awake() {
			spawnPointStrategy = spawnPointStrategyType switch {
				SpawnPointStrategyType.Linear => new LinearSpawnPointStrategy(spawnPoints),
				SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(spawnPoints),
				_ => spawnPointStrategy
			};
		}

		private void Start() {
			if (spawnOnStart) Spawn();
		}
		
		public abstract void Spawn();
	}
}