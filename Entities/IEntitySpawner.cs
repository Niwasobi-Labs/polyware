using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntitySpawner<out T> where T : IEntity {
		T Spawn(Transform spawnPoint);
	}
}