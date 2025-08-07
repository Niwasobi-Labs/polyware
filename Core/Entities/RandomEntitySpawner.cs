using UnityEngine;

namespace PolyWare.Core.Entities {
	public class RandomEntitySpawner<T, T2, T3> : IEntitySpawner<T> where T : IEntity where T2 : EntityDefinition where T3 : IEntitySpawnData {
		private readonly EntitySpawnerData<T2, T3>[] data;
		
		public RandomEntitySpawner(EntitySpawnerData<T2, T3>[] data) {
			this.data = data;
		}
		
		public T Spawn(Transform spawnPoint) {
			return EntityFactory<T>.CreateWith(data[Random.Range(0, data.Length)], spawnPoint.position, spawnPoint.rotation);
		}
	}
}