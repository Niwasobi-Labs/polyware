
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Abilities {
	public class AbilityContextHolder : ContextHolder {
		public AbilityDefinition AbilityDefinition;
		public GameObject Culprit;
		public readonly List<GameObject> Targets;
		
		public AbilityContextHolder(AbilityDefinition abilityDefinition, GameObject culprit, List<GameObject> targets = null, List<IContext> contexts = null) : base(contexts) {
			AbilityDefinition = abilityDefinition;
			Culprit = culprit;
			Targets = targets ?? new List<GameObject>();
		}
	}
}