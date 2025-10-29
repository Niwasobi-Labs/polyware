using PolyWare.Interactions;

namespace PolyWare.Items {
	public interface IPickupable {
		public bool AutoPickup { get; }
		public void Pickup(IProximityUser user);
	}
}