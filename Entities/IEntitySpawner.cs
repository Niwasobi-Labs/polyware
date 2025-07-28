using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntitySpawner<out T> where T : Entity {
		T Spawn(Transform spawnPoint);
	}
}