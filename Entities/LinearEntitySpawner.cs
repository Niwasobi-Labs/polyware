using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntitySpawner<T, T2, T3> : IEntitySpawner<T> where T : IEntity where T2 : EntityDefinition where T3 : ISpawnData {
		
		private readonly EntitySpawnData<T2, T3>[] data;
		private int index;
		
		public LinearEntitySpawner(EntitySpawnData<T2, T3>[] data) {
			this.data = data;
		}
		
		public T Spawn(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			return EntityFactory<T>.CreateWith(data[index++], spawnPoint.position, spawnPoint.rotation);
		}
	}
}