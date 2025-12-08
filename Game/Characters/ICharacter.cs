using UnityEngine;

namespace PolyWare.Game {
	public interface ICharacter {
		public void Interact();
		public bool Pickup(IPickupable item);
		public GameObject GameObject { get; }
		public Transform Transform { get; }
		public IStatsHandler Stats { get; }
		public IEffectsHandler Effects { get; }
		public IFactionMember FactionMember { get; }
		public CharacterMoveSettings MoveSettings { get; }
		public void Kill(DamageContext damageContext);
	}
}