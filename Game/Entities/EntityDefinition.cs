using UnityEngine;

namespace PolyWare.Game {
	public abstract class EntityDefinition : ScriptableObject {
		public GameObject Prefab;
		public abstract IEntityData CreateDefaultInstance();
	}
}