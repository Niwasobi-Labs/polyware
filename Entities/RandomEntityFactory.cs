using UnityEngine;

namespace PolyWare.Entities {
	public class RandomEntityFactory<T> : IEntityFactory<T> where T : Entity {
		private readonly SpawnOverrideData[] data;
		
		public RandomEntityFactory(SpawnOverrideData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			SpawnOverrideData spawnOverrideData = data[Random.Range(0, data.Length)];
			GameObject instance = Object.Instantiate(spawnOverrideData.EntityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			var component = instance.GetComponent<T>();
			if (component is IAllowSpawnOverride spawnOverride && spawnOverrideData.Override) spawnOverride.OnSpawn(spawnOverrideData);
			return component;
		}
	}
}