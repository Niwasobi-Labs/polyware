using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class SimpleSpawnEntityManager : MonoBehaviour {
		[SerializeField] private List<SpawnEntity> spawnEntities;

		public void Spawn() {
			for (int i = 0; i < spawnEntities.Count; i++) {
				spawnEntities[i].Spawn();
			}
		}
	}
}