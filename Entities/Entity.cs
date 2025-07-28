using UnityEngine; 

namespace PolyWare.Entities {
	public interface IEntity {
		public EntityData Data { get; }
		public GameObject GameObject { get; }
		void Initialize(EntityData data);
	}

	public abstract class Entity : MonoBehaviour, IEntity {
		public abstract EntityData Data { get; }
		public GameObject GameObject => gameObject;
		public abstract void Initialize(EntityData data);
	}
}