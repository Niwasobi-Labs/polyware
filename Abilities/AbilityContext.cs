using System.Collections.Generic;
using PolyWare.Core;
using PolyWare.Core.Entities;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	public class AbilityContext : IContext {
		public Ability Ability;
		public int Faction;
		public GameObject Owner;
		public List<GameObject> Targets;
		public Vector3 Position;
		public Quaternion Rotation;
		
		public AbilityContext(Ability ability, int faction, GameObject owner, List<GameObject> targets, Vector3 position, Quaternion rotation) {
			Ability = ability;
			Faction = faction;
			Owner = owner;
			Targets = targets;
			Position = position;
			Rotation = rotation;
		}
	}
}