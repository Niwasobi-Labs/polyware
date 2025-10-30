using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class RandomSpawnPointStrategy : ISpawnPointStrategy {
		private readonly Transform[] spawnPoints;
		private List<Transform> unusedSpawnPoints;
		private int index;

		public RandomSpawnPointStrategy(Transform[] spawnPoints) {
			this.spawnPoints = spawnPoints;
			this.spawnPoints.Shuffle();
		}

		public SpawnLocation NextSpawnPoint() {
			if (index < spawnPoints.Length) return new SpawnLocation(spawnPoints[index++]);
			
			index = 0;
			spawnPoints.Shuffle();
			return new SpawnLocation(spawnPoints[index++]);
		}
	}
}