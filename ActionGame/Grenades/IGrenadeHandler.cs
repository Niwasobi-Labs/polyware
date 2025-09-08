using PolyWare.ActionGame.Grenades;
using PolyWare.Inventory;
using UnityEngine.Events;

namespace PolyWare.ActionGame {
	public interface IGrenadeHandler {
		event UnityAction<InventorySlot<GrenadeDefinition>> OnActiveGrenadeUpdate;
		bool Pickup(Grenade grenade);
		bool Throw();
		bool Swap();
	}
}