using UnityEngine;

namespace PolyWare.Core.Entities {
	public interface IEntitySpawner<out T> where T : IEntity {
		T Spawn(Transform spawnPoint);
	}
}