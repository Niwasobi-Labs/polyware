using PolyWare.Entities;

namespace PolyWare.Spawning {
	public class EntitySpawner<T> where T : IEntity {
		private readonly IEntitySpawner<T> entitySpawner;
		private readonly ISpawnPointStrategy spawnPointStrategy;

		public EntitySpawner(IEntitySpawner<T> entitySpawner, ISpawnPointStrategy spawnPointStrategy) {
			this.entitySpawner = entitySpawner;
			this.spawnPointStrategy = spawnPointStrategy;
		}
		
		public T Spawn() {
			return entitySpawner.Spawn(spawnPointStrategy.NextSpawnPoint());
		}
	}
}