using System;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Game {
	[Serializable]
	public class SlottedWeaponHandler : IWeaponHandler {

		[ShowInInspector] public EntitySlotInventory<Weapon, WeaponData> Slots { get; private set; }
		[ShowInInspector] public Weapon CurrentWeapon { get; private set; }

		private readonly ICharacter character;
		private readonly Transform pivot;

		public event UnityAction<Weapon> OnEquip = delegate { };
		public event UnityAction<Weapon> OnUnequip = delegate { };
		
		public SlottedWeaponHandler(ICharacter character, Transform pivot, uint slots) {
			this.character = character;
			this.pivot = pivot;
			Slots = new EntitySlotInventory<Weapon, WeaponData>((int)slots);
		}

		public bool Pickup(Weapon newWeapon) {
			if (CurrentWeapon != null && !CurrentWeapon.Unequip()) return false;

			int slotAddedTo = Slots.Add(newWeapon.Data);

			if (slotAddedTo < 0) {
				Log.Error("Could not add equipment to inventory");
				return false;
			}

			Slots.SetActiveSlot(slotAddedTo);
			
			Equip(newWeapon);

			return true;
		}

		public void Equip(Weapon weapon) {
			CurrentWeapon = weapon;

			CurrentWeapon.transform.SetParent(pivot, false);
			CurrentWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

			// OnEquip should be called before Equipment.Equip so that events triggered by Equipment.Equip can be
			// properly subscribed to.
			// e.g., if a Gun needs to reload on Equip (empty clip), the UI needs to subscribe first
			OnEquip.Invoke(CurrentWeapon);
			
			CurrentWeapon.Equip(character);
		}

		public void Use() {
			CurrentWeapon?.Use();
		}

		public void ForceUse() {
			CurrentWeapon?.ForceUse();
		}

		public void StopUsing() {
			CurrentWeapon?.StopUsing();
		}

		public bool DropCurrent() {
			if (!CurrentWeapon) return true;

			if (!CurrentWeapon.Drop()) {
				Log.Error("Could not drop current equipment");
				return false;
			}
			
			OnUnequip.Invoke(CurrentWeapon);
			CurrentWeapon = null;
			Slots.ClearActiveSlot();
			return true;
		}

		public bool Cycle(bool left = false) {
			int previousIndex = Slots.ActiveIndex;

			if (left ? !Slots.CycleLeft() : !Slots.CycleRight()) return false;

			if (CurrentWeapon != null && !CurrentWeapon.Unequip()) {
				Slots.SetActiveSlot(previousIndex);
				return false;
			}

			if (Slots.ActiveItem == null) return true;

			Equip(EntityFactory<Weapon>.CreateWith(Slots.ActiveItem));

			return true;
		}
		
		public bool NeedsAmmoForType(string itemID) {
			foreach (WeaponData equipment in Slots.Slots) {
				if (equipment != null && equipment.Definition.ItemID == itemID && equipment is GunData gun && gun.NeedsAmmo()) {
					return true;
				}
			}
			return false;
		}
		
		public int AddAmmo(string itemID, int ammoCount) {
			int ammoLeft = ammoCount;
			
			foreach (WeaponData equipment in Slots.Slots) {
				if (ammoLeft == 0) break;

				if (equipment.Definition.ItemID != itemID || equipment is not GunData gun || !gun.NeedsAmmo()) continue;
				
				ammoLeft = gun.AddAmmoToReserves(ammoLeft);
				
				// bug: this needs to properly handle local and networked multiplayer (https://app.clickup.com/t/86b6wajfh)
				if (character.FactionMember.FactionID == 0) ServiceLocator.Global.Get<IAudioService>().PlayOneShot(gun.GunDefinition.ReloadingSfx, character.Transform.position);
			}
			
			return ammoLeft;
		}
	}
}