using UnityEngine.Events;

namespace PolyWare.Game {
	public interface IGrenadeHandler {
		event UnityAction<InventorySlot<GrenadeDefinition>> OnActiveGrenadeUpdate;
		bool Pickup(Grenade grenade);
		bool Throw();
		bool Swap();
	}
}