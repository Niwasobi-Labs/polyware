using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntityFactory<T> where T : Entity {
		T Create(Transform spawnPoint);
	}
}