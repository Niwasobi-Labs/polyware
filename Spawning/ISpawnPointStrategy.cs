using UnityEngine;

namespace PolyWare.Spawning {
	public interface ISpawnPointStrategy {
		public Transform NextSpawnPoint();
	}
}