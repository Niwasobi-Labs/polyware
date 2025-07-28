using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntitySpawner<TEntity, TData> : IEntitySpawner<TEntity> where TEntity : Entity where TData : IEntitySpawnData {
		
		private readonly TData[] data;
		private int index;
		
		public LinearEntitySpawner(TData[] data) {
			this.data = data;
		}
		
		public TEntity Spawn(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			return EntityFactory<TEntity>.Create(data[index++], spawnPoint.position, spawnPoint.rotation);
		}
	}
}