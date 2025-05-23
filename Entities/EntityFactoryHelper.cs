using UnityEngine;

namespace PolyWare.Entities {
	public static class EntityFactoryHelper<T, T2> where T : Entity where T2 : EntityData {
		public static T Create(Transform spawnPoint, T2 entityData) {
			GameObject instance = Object.Instantiate(entityData.Prefab, spawnPoint.position, spawnPoint.rotation);
			return instance.GetComponent<T>();
		}
	}
}