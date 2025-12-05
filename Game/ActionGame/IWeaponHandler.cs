using UnityEngine.Events;

namespace PolyWare.Game {
	public interface IWeaponHandler {
		event UnityAction<Weapon> OnEquip;
		event UnityAction<Weapon> OnUnequip;
		bool Pickup(Weapon newWeapon);
		void Equip(Weapon weapon);
		void Use();
		void StopUsing();
		bool DropCurrent();
		bool Cycle(bool left = false);
		bool NeedsAmmoForType(string itemID);
		int AddAmmo(string itemID, int ammoCount);
	}
}