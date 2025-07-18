using PolyWare.Interactions;

namespace PolyWare.Items {
	public interface IPickupable {
		public void Pickup(IProximityUser user);
	}
}