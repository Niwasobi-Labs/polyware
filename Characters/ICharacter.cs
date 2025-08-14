using PolyWare.Interactions;
using PolyWare.Items;
using UnityEngine;

namespace PolyWare.Characters {
	public interface ICharacter {
		public void Interact();
		public bool Pickup(IPickupable item);
		public Transform Transform { get; }
	}
}