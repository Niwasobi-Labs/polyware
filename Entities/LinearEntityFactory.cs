using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntityFactory<T> : IEntityFactory<T> where T : Entity {
		
		private readonly SpawnOverrideData[] data;
		private int index;
		
		public LinearEntityFactory(SpawnOverrideData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			
			SpawnOverrideData spawnOverrideData = data[index++];
			GameObject instance = Object.Instantiate(spawnOverrideData.EntityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			var component = instance.GetComponent<T>();
			if (component is IAllowSpawnOverride spawnOverride && spawnOverrideData.Override) spawnOverride.OnSpawn(spawnOverrideData);
			return component;
		}
	}
}