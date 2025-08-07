using UnityEngine; 

namespace PolyWare.Core.Entities {
	public interface IEntity {
		public IEntityData EntityData { get; }
		public GameObject GameObject { get; }
		void Initialize(IEntityData data);
	}

	public abstract class Entity<T> : MonoBehaviour, IEntity where T : IEntityData {
		public IEntityData EntityData => Data;
		public GameObject GameObject => gameObject;
		
		public abstract T Data { get; protected set; }
		
		public void Initialize(IEntityData data) {
			if (data is not T entityData) throw new System.ArgumentException($"Invalid data type for {GetType().Name}");
			Data = entityData;
			OnInitialize();
		}
		
		protected abstract void OnInitialize();
	}
}