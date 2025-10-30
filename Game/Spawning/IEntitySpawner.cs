namespace PolyWare.Game {
	public interface IEntitySpawner<out T> where T : IEntity {
		T Spawn(SpawnLocation spawnPointStrategy);
	}
}