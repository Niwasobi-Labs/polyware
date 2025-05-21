using UnityEngine;

namespace PolyWare.Entities {
	public class RandomEntityFactory<T> : IEntityFactory<T> where T : Entity {
		private readonly SpawnData[] data;
		
		public RandomEntityFactory(SpawnData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			SpawnData spawnData = data[Random.Range(0, data.Length)];
			GameObject instance = Object.Instantiate(spawnData.EntityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			var component = instance.GetComponent<T>();
			if (component is IAllowSpawnOverride spawnOverride && spawnData.Override) spawnOverride.OnSpawn(spawnData);
			return component;
		}
	}
}