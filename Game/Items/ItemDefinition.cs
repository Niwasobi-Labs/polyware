using PolyWare.Core;

namespace PolyWare.Game {
	public struct ItemPickedUpEvent : IEvent {
		public ItemDefinition ItemDefinition;
	}
	
	public abstract class ItemDefinition : EntityDefinition {
		public string ItemID; // todo: make an enum of sorts
		public string Name;
		public string Description;
		public string PickupMessage;
		public int MaxStack = 1;
	}
}