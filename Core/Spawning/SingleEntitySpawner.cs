using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.Core.Spawning {
	// todo: expand this to support a drop down of any type of entity and entity spawn data
	public class SingleEntitySpawner : MonoBehaviour, IEntitySpawner<IEntity> {
		
		public bool SpawnOnStart = false;
		public EntityDefinition EntityToSpawn;

		private void Start() {
			if (SpawnOnStart) Spawn(transform);
		}

		public IEntity Spawn(Transform spawnPoint) {
			return EntityFactory<IEntity>.CreateFrom(EntityToSpawn, spawnPoint.position, spawnPoint.rotation);
		}
	}
}