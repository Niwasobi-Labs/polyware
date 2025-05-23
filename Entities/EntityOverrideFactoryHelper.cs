using UnityEngine;

namespace PolyWare.Entities {
	public static class EntityOverrideFactoryHelper<T> where T : IAllowSpawnOverride {
		public static T Create(Transform spawnPoint, EntityOverrideData overrideData) {
			Entity entity = EntityFactoryHelper<Entity, EntityData>.Create(spawnPoint, overrideData.EntityData);
			var spawnOverride = entity.gameObject.GetComponent<T>();
			if (overrideData.Override) spawnOverride.OnSpawnOverride(overrideData);
			return spawnOverride;
		}
	}
}