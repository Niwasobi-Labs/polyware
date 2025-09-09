using PolyWare.Core.Spawning;

namespace PolyWare.Core.Entities {
	public interface IEntitySpawner<out T> where T : IEntity {
		T Spawn(SpawnLocation spawnPointStrategy);
	}
}