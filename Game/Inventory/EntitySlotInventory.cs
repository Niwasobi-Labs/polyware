using System;

namespace PolyWare.Game {
	[Serializable]
	public class EntitySlotInventory<TEntity, TData> : SlotInventory<TData> where TEntity : Entity<TData> where TData : IEntityData {
		public EntitySlotInventory(int capacity) : base(capacity) { }
		
		public TEntity DropAt(int index) {
			TEntity droppedEntity = EntityFactory<TEntity>.CreateWith(Slots[index]);
			ClearSlot(index);
			return droppedEntity;
		}
	}
}