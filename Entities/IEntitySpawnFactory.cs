using UnityEngine;

namespace PolyWare.Entities {
	public static class EntitySpawnFactory<T, T2> where T : IAllowSpawnOverride where T2 : SpawnData {
		public static T Create(Transform spawnPoint, T2 spawnData) {
			var entity = Object.Instantiate(spawnData.EntityData.Prefab, spawnPoint.position, spawnPoint.rotation).GetComponent<T>();
			if (spawnData.Override) entity.OnSpawn(spawnData);
			return entity;
		}
	}
}