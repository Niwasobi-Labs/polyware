using UnityEngine;

namespace PolyWare.Core.Entities {
	public abstract class EntityDefinition : ScriptableObject {
		public GameObject Prefab;
		public abstract IEntityData CreateDefaultInstance();
	}
}