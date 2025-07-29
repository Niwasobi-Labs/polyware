using UnityEngine;

namespace PolyWare.Entities {
	public static class EntityFactory<T> where T : IEntity {
		
		public static T Create(EntityDefinition entityDefinition, Vector3 position = default, Quaternion rotation = default) {
			var instance = Object.Instantiate(entityDefinition.Prefab, position, rotation).GetComponent<T>();
			instance.Initialize(entityDefinition.CreateDefaultInstance());
			return instance;
		}

		public static T Create(IEntityData dataReference, Vector3 position = default, Quaternion rotation = default) {
			T newEquipment = Create(dataReference.EntityDefinition, position, rotation);
			newEquipment.Initialize(dataReference);
			return newEquipment;
		}
		
		public static T Create(IEntitySpawnData spawnData, Vector3 position = default, Quaternion rotation = default) {
			var instance = Object.Instantiate(spawnData.Definition.Prefab, position, rotation).GetComponent<T>();
			IEntityData newData = spawnData.Definition.CreateDefaultInstance();
			EntityHelpers.ApplyOverrides(newData, spawnData.OverrideData);
			instance.Initialize(newData);
			return instance;
		}
	}
}