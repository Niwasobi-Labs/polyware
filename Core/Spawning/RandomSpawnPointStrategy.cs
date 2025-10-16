using System.Collections.Generic;
using System.Numerics;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Core.Spawning {
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