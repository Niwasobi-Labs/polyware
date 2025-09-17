using PolyWare.Items;
using PolyWare.Stats;
using UnityEngine;

namespace PolyWare.Characters {
	public interface ICharacter {
		public void Interact();
		public bool Pickup(IPickupable item);
		public Transform Transform { get; }
		public IStatsHandler Stats { get; }
		public bool IsPlayer { get; }
	}
}