using PolyWare.Characters;
using PolyWare.Items;
using PolyWare.ActionGame.Grenades;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.ActionGame {
	public class CharacterInventoryHandler : MonoBehaviour {

		public Transform EquipmentPivot;
		public WeaponHandler Weapons;
		public GrenadeHandler Grenades;	
		
		public event UnityAction<IPickupable> OnPickup = delegate {};

		public void Initialize(ICharacter newCharacter, uint weaponSlots) {
			Weapons = new WeaponHandler(newCharacter, EquipmentPivot, weaponSlots);
			Grenades = new GrenadeHandler(newCharacter);
		}
		
		public bool Pickup(IPickupable pickupable) {
			bool successfulPickup = pickupable switch {
				Weapon equipment => Weapons.Pickup(equipment),
				Grenade grenade => Grenades.Pickup(grenade),
				_ => false
			};

			if (!successfulPickup) {
				return false;
			}
			
			OnPickup.Invoke(pickupable);
			return true;
		}
	}
}