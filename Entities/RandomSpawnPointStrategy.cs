using System.Collections.Generic;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Entities {
	public class RandomSpawnPointStrategy : ISpawnPointStrategy {
		private readonly Transform[] spawnPoints;
		private List<Transform> unusedSpawnPoints;
		private int index;

		public RandomSpawnPointStrategy(Transform[] spawnPoints) {
			this.spawnPoints = spawnPoints;
			this.spawnPoints.Shuffle();
		}

		public Transform NextSpawnPoint() {
			if (index < spawnPoints.Length) return spawnPoints[index++];
			
			index = 0;
			spawnPoints.Shuffle();
			return spawnPoints[index++];
		}
	}
}