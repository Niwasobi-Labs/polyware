using System.Collections;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Core.Spawning {
	public abstract class SpawnManager : MonoBehaviour {
		[SerializeField] private bool spawnOnStart = false;
		[SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
		[SerializeField] protected Transform[] spawnPoints;
		[SerializeField] protected uint spawnCount;
		[SerializeField] protected bool random;
		[SerializeField] protected bool loop;
		[SerializeField] protected float loopDelay;
		
		protected ISpawnPointStrategy spawnPointStrategy;
		[Header("Random Radius Only")]
		[SerializeField] protected float innerRadius;
		[SerializeField] protected float outerRadius;

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

		public virtual void Spawn() {
			if (loop) {
				StartCoroutine(SpawnDelay());
			}
		}
		
		private IEnumerator SpawnDelay() {
			yield return Yielders.WaitForSeconds(loopDelay);
			Spawn();
		}
	}
}