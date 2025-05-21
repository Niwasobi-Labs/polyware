using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntityFactory<out T> where T : Entity {
		T Create(Transform spawnPoint);
	}
}