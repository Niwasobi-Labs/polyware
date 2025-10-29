using System.Collections;
using System.Numerics;
using PolyWare.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PolyWare.Core.Spawning {
	public abstract class SpawnManager : MonoBehaviour {
		[SerializeField] private bool spawnOnStart = false;
		
		[Title("Spawn Locations")]
		[SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
		[SerializeField] [field: ShowIf("spawnPointStrategyType", SpawnPointStrategyType.RandomRadiusOnAxis)] protected float innerRadius;
		[SerializeField] [field: ShowIf("spawnPointStrategyType", SpawnPointStrategyType.RandomRadiusOnAxis)] protected float outerRadius;
		[SerializeField] [field: ShowIf("@spawnPointStrategyType == SpawnPointStrategyType.Linear || spawnPointStrategyType == SpawnPointStrategyType.Random")] protected Transform[] spawnPoints;
		
		[Title("Spawn Data")]
		[SerializeField] protected uint spawnCount;
		[SerializeField] protected bool random;
		[SerializeField] protected bool loop;
		[SerializeField] protected float loopDelay;
		
		protected ISpawnPointStrategy spawnPointStrategy;

		protected enum SpawnPointStrategyType {
			Linear,
			Random,
			RandomRadiusOnAxis,
			RandomScreenPoint
		}

		protected virtual void Awake() {
			spawnPointStrategy = spawnPointStrategyType switch {
				SpawnPointStrategyType.Linear => new LinearSpawnPointStrategy(spawnPoints),
				SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(spawnPoints),
				SpawnPointStrategyType.RandomRadiusOnAxis => new RandomRadiusAlongAxisSpawnPointStrategy(transform.position, innerRadius, outerRadius, transform.up),
				SpawnPointStrategyType.RandomScreenPoint => new RandomWithinScreenSpawnPointStrategy(transform.up),
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