using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class SimpleSpawnEntityManager : MonoBehaviour {
		private readonly List<SpawnEntity> spawnEntities = new List<SpawnEntity>();

		private void FindSpawners() {
			GetComponentsInChildren<SpawnEntity>(true, spawnEntities);
		}

		public void Spawn() {
			FindSpawners();
			
			for (int i = 0; i < spawnEntities.Count; i++) {
				spawnEntities[i]?.Spawn();
			}
		}
	}
}