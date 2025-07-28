using UnityEngine;

namespace PolyWare.Entities {
	public class RandomEntitySpawner<TEntity, TData> : IEntitySpawner<TEntity> where TEntity : Entity where TData : IEntitySpawnData {
		private readonly TData[] data;
		
		public RandomEntitySpawner(TData[] data) {
			this.data = data;
		}
		
		public TEntity Spawn(Transform spawnPoint) {
			return EntityFactory<TEntity>.Create(data[Random.Range(0, data.Length)], spawnPoint.position, spawnPoint.rotation);
		}
	}
}