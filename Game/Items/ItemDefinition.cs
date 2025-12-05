using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public struct ItemPickedUpEvent : IEvent {
		public Vector3 Position;
		public ItemDefinition ItemDefinition;
	}
	
	public struct ItemInRangeEvent : IEvent {
		public Vector3 Position;
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