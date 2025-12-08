using UnityEngine;

namespace PolyWare.Game {
	// todo: expand this to support a drop down of any types of entity and entity spawn data
	public class SingleEntitySpawner : MonoBehaviour, IEntitySpawner<IEntity> {
		
		public bool SpawnOnStart = false;
		public EntityDefinition EntityToSpawn;

		private void Start() {
			if (SpawnOnStart) Spawn(new SpawnLocation(transform));
		}

		public IEntity Spawn(SpawnLocation spawnPoint) {
			return EntityFactory<IEntity>.CreateFrom(EntityToSpawn, spawnPoint.Position, spawnPoint.Rotation);
		}
	}
}