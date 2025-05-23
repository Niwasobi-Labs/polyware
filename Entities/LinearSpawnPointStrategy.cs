using UnityEngine;

namespace PolyWare.Entities {
	public class LinearSpawnPointStrategy : ISpawnPointStrategy {
		private readonly Transform[] spawnPoints;
		private int index = 0;

		public LinearSpawnPointStrategy(Transform[] spawnPoints) {
			this.spawnPoints = spawnPoints;
		}

		public Transform NextSpawnPoint() {
			Transform spawnPoint = spawnPoints[index];
			index = (index + 1) % spawnPoints.Length;
			return spawnPoint;
		}
	}
}