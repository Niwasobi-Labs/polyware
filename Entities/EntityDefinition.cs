using UnityEngine;

namespace PolyWare.Entities {
	public abstract class EntityDefinition : ScriptableObject {
		public GameObject Prefab;
		public abstract IEntityData CreateDefaultInstance();
	}
}