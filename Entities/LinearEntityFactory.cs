using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntityFactory<T> : IEntityFactory<T> where T : Entity {
		
		private readonly EntityData[] data;
		private int index;
		
		public LinearEntityFactory(EntityData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			EntityData entityData = data[index++];
			GameObject instance = Object.Instantiate(entityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			return instance.GetComponent<T>();
		}
	}
}