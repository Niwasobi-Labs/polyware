using UnityEngine;

namespace PolyWare.Core.Spawning {
	public abstract class SpawnManager : MonoBehaviour {
		[SerializeField] private bool spawnOnStart = false;
		[SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
		[SerializeField] protected Transform[] spawnPoints;
		[SerializeField] protected uint spawnCount;
		[SerializeField] protected bool random;
		
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