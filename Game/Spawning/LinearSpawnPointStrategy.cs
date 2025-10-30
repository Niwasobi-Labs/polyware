using UnityEngine;

namespace PolyWare.Game {
	public class LinearSpawnPointStrategy : ISpawnPointStrategy {
		private readonly Transform[] spawnPoints;
		private int index = 0;

		public LinearSpawnPointStrategy(Transform[] spawnPoints) {
			this.spawnPoints = spawnPoints;
		}

		public SpawnLocation NextSpawnPoint() {
			Transform spawnPoint = spawnPoints[index];
			index = (index + 1) % spawnPoints.Length;
			return new SpawnLocation(spawnPoint);
		}
	}
}