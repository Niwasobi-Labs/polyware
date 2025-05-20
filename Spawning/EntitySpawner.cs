using PolyWare.Entities;
using PolyWare.Gameplay;

namespace PolyWare.Spawning {
	public class EntitySpawner<T> where T : Entity {
		private readonly IEntityFactory<T> entityFactory;
		private readonly ISpawnPointStrategy spawnPointStrategy;

		public EntitySpawner(IEntityFactory<T> entityFactory, ISpawnPointStrategy spawnPointStrategy) {
			this.entityFactory = entityFactory;
			this.spawnPointStrategy = spawnPointStrategy;
		}
		
		public T Spawn() {
			return entityFactory.Create(spawnPointStrategy.NextSpawnPoint());
		}
	}
}