using UnityEngine; 

namespace PolyWare.Game {
	public interface IEntity {
		public IEntityData EntityData { get; }
		public GameObject GameObject { get; }
		void Initialize(IEntityData data);
	}

	public abstract class Entity<T> : MonoBehaviour, IEntity where T : IEntityData {
		public IEntityData EntityData => Data;
		public GameObject GameObject => gameObject;
		
		// todo: give entities a required Definition field to allow manual placement of entities (https://app.clickup.com/t/86b6vaqdv)
		public abstract T Data { get; protected set; }

		public void Initialize(IEntityData data) {
			if (data is not T entityData) throw new System.ArgumentException($"Invalid data types for {GetType().Name}");
			Data = entityData;
			OnInitialize();
		}
		
		// called after setting up default EntityData
		protected abstract void OnInitialize();
	}

	public abstract class Entity : Entity<EntityData> {
		public override EntityData Data { get; protected set; }
		
		protected override void OnInitialize() {

		}
	}
}