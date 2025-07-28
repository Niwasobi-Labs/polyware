using UnityEngine;

namespace PolyWare.Entities {
	public interface IEntityDefinition { }

	public abstract class EntityDefinition : ScriptableObject, IEntityDefinition {
		public GameObject Prefab;

		public abstract EntityData CreateDefaultInstance();
	}
}