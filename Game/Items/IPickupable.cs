namespace PolyWare.Game {
	public interface IPickupable {
		public bool AutoPickup { get; }
		public void Pickup(IProximityUser user);
	}
}