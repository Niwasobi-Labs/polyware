using System;
using PolyWare.ActionGame.Grenades;
using PolyWare.Characters;
using PolyWare.Core.Entities;
using PolyWare.Interactions;
using PolyWare.Inventory;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace PolyWare.ActionGame {
	[Serializable]
	public class GrenadeHandler {
		// todo: standardize this to clean up this script and offloading inventory management
		[ShowInInspector] public DynamicInventory<InventorySlot<GrenadeDefinition>> Inventory { get; private set; }
		
		private readonly ICharacter character;
		
		public event UnityAction<InventorySlot<GrenadeDefinition>> OnActiveGrenadeUpdate = delegate {};

		public GrenadeHandler(ICharacter character) {
			this.character = character;
			Inventory = new DynamicInventory<InventorySlot<GrenadeDefinition>>(5);
		}
		
		public bool Pickup(Grenade grenade) {
			int slotToAddTo = GetSlotIndexFor(grenade.Data.Definition);
			
			if (slotToAddTo < 0) {
				var newSlot = new InventorySlot<GrenadeDefinition>(grenade.Data.Definition, 1);
				slotToAddTo = Inventory.Add(newSlot);
			}
			else {
				if (Inventory.Items[slotToAddTo].Stack >= grenade.Data.Definition.MaxStack) return false;
				Inventory.Items[slotToAddTo].Stack++;
			}
			
			grenade.Pickup(character as IProximityUser);
			
			if (Inventory.ActiveSlot == null || Inventory.ActiveSlot.Stack == 0) Inventory.SetActiveSlot(slotToAddTo);
			if (Inventory.ActiveSlot?.Item.ItemID == grenade.Data.Definition.ItemID) OnActiveGrenadeUpdate.Invoke(Inventory.ActiveSlot);
			
			return true;
		}

		public bool Throw() {
			if (Inventory.ActiveSlot == null) return false;
			
			Grenade newGrenade = EntityFactory<Grenade>.CreateFrom(Inventory.ActiveSlot.Item);
			newGrenade.transform.forward = character.Transform.forward;
			newGrenade.transform.position = character.Transform.position + (character.Transform.forward * 1.5f);
			newGrenade.Throw();
			
			Inventory.ActiveSlot.Stack--;

			if (Inventory.ActiveSlot.Stack == 0) Inventory.SetActiveSlot(FindNextNonEmptySlot());
			
			OnActiveGrenadeUpdate.Invoke(Inventory.ActiveSlot);
			return true;
		}

		private int GetSlotIndexFor(GrenadeDefinition grenade) {
			for (int i = 0; i < Inventory.Size; i++) {
				if (Inventory.Items[i].Item.ItemID == grenade.ItemID) return i;
			}

			return -1;
		}

		private int FindNextNonEmptySlot() {
			if (Inventory.ActiveIndex < 0) return -1;
			
			int startIndex = Inventory.ActiveIndex;
			int index = (startIndex + 1) % Inventory.Size;

			while (index != startIndex) {
				var slot = Inventory.Items[index];
				if (slot != null && slot.Stack > 0) {
					return index;
				}

				index = (index + 1) % Inventory.Size;
			}

			return startIndex;
		}
		
		public bool Swap() {
			int newSlot = FindNextNonEmptySlot(); 
			if (newSlot == Inventory.ActiveIndex) return false;
			
			Inventory.SetActiveSlot(newSlot);
			OnActiveGrenadeUpdate.Invoke(Inventory.ActiveSlot);
			return true;
		}
	}
}