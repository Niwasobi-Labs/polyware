using PolyWare.Interactions;
using PolyWare.Items;

namespace PolyWare.Characters {
	public interface ICharacter : IProximityUser {
		public void Interact();
		public bool Pickup(IPickupable item);
		public bool IsPlayer { get; }
	}
}