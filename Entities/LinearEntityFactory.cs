using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntityFactory<T> : IEntityFactory<T> where T : Entity {
		
		private readonly SpawnData[] data;
		private int index;
		
		public LinearEntityFactory(SpawnData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			SpawnData spawnData = data[index++];
			GameObject instance = Object.Instantiate(spawnData.EntityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			var component = instance.GetComponent<T>();
			if (component is IAllowSpawnOverride spawnOverride && spawnData.Override) spawnOverride.OnSpawn(spawnData);
			return component;
		}
	}
}