using UnityEngine;

namespace PolyWare.Entities {
	public interface ISpawnPointStrategy {
		public Transform NextSpawnPoint();
	}
}