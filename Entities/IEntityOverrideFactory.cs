using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntityOverrideFactory<out T> where T : IAllowSpawnOverride {
		T Create(Transform spawnPoint);
	}
}