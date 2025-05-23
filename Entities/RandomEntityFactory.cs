using UnityEngine;

namespace PolyWare.Entities {
	public class RandomEntityFactory<T> : IEntityFactory<T> where T : Entity {
		private readonly EntityData[] data;
		
		public RandomEntityFactory(EntityData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			EntityData entityData = data[Random.Range(0, data.Length)];
			GameObject instance = Object.Instantiate(entityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			return instance.GetComponent<T>();
		}
	}
}