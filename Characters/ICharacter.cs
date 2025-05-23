using PolyWare.Interaction;
using PolyWare.Items;

namespace PolyWare.Characters {
	public interface ICharacter : IProximityUser {
		public void Interact();
		public bool Pickup(IItem item);
		public bool IsPlayer { get; }
	}
}