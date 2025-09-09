using UnityEngine;

namespace PolyWare.Core.Spawning {
	public abstract class SpawnManager : MonoBehaviour {
		[SerializeField] private bool spawnOnStart = false;
		[SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
		[SerializeField] protected Transform[] spawnPoints;
		[SerializeField] protected uint spawnCount;
		[SerializeField] protected bool random;
		
		protected ISpawnPointStrategy spawnPointStrategy;
		[Header("Random Radius Only")]
		[SerializeField] private float innerRadius;
		[SerializeField] private float outerRadius;

		protected enum SpawnPointStrategyType {
			Linear,
			Random,
			RandomRadiusOnAxis
		}

		protected virtual void Awake() {
			spawnPointStrategy = spawnPointStrategyType switch {
				SpawnPointStrategyType.Linear => new LinearSpawnPointStrategy(spawnPoints),
				SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(spawnPoints),
				SpawnPointStrategyType.RandomRadiusOnAxis => new RandomRadiusAlongAxisSpawnPointStrategy(transform.position, innerRadius, outerRadius, transform.up),
				_ => spawnPointStrategy
			};
		}

		private void Start() {
			if (spawnOnStart) Spawn();
		}
		
		public abstract void Spawn();
	}
}