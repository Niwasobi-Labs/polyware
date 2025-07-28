using PolyWare.Interactions;
using PolyWare.Items;

namespace PolyWare.Characters {
	public interface ICharacter {
		public void Interact();
		public bool Pickup(IPickupable item);
	}
}