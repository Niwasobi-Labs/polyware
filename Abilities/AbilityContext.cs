using PolyWare.Combat;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Abilities {
	public class AbilityContext : ContextHolder {
		public Ability Ability;
		public int Faction;
		public GameObject Owner;
		public Vector3 Position;
		public Quaternion Rotation;
		
		public AbilityContext(Ability ability, int faction, GameObject owner, Vector3 position, Quaternion rotation) {
			Ability = ability;
			Faction = faction;
			Owner = owner;
			Position = position;
			Rotation = rotation;
		}
	}
}