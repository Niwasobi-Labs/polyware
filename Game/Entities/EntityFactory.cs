using UnityEngine;

namespace PolyWare.Game {
	public static class EntityFactory<T> where T : IEntity {
		public static T CreateFrom(EntityDefinition entityDefinition, Vector3 position = default, Quaternion rotation = default) {
			var instance = Object.Instantiate(entityDefinition.Prefab, position, rotation).GetComponent<T>();
			instance.Initialize(entityDefinition.CreateDefaultInstance());
			return instance;
		}

		public static T CreateWith(IEntityData dataReference, Vector3 position = default, Quaternion rotation = default) {
			T newEntity = CreateFrom(dataReference.EntityDefinition, position, rotation);
			newEntity.Initialize(dataReference);
			return newEntity;
		}
		
		public static T CreateWith(IEntitySpawnerData spawnData, Vector3 position = default, Quaternion rotation = default) {
			var instance = Object.Instantiate(spawnData.Definition.Prefab, position, rotation).GetComponent<T>();
			IEntityData newData = spawnData.Definition.CreateDefaultInstance();
			if (newData is IAllowSpawnOverride overrideable) overrideable.Override(spawnData.Data);
			instance.Initialize(newData);
			return instance;
		}
	}
}