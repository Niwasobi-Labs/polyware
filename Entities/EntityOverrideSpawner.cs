namespace PolyWare.Entities {
	public class EntityOverrideSpawner<T> where T : IAllowSpawnOverride {
		private readonly IEntityOverrideFactory<T> entityFactory;
		private readonly ISpawnPointStrategy spawnPointStrategy;

		public EntityOverrideSpawner(IEntityOverrideFactory<T> entityFactory, ISpawnPointStrategy spawnPointStrategy) {
			this.entityFactory = entityFactory;
			this.spawnPointStrategy = spawnPointStrategy;
		}
		
		public T Spawn() {
			return entityFactory.Create(spawnPointStrategy.NextSpawnPoint());
		}
	}
}