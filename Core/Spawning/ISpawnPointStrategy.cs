using UnityEngine;

namespace PolyWare.Core.Spawning {
	public interface ISpawnPointStrategy {
		public Transform NextSpawnPoint();
	}
}