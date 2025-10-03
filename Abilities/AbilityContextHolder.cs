
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Abilities {
	public class AbilityContextHolder : ContextHolder {
		public AbilityDefinition AbilityDefinition;
		public GameObject Caster;
		public readonly List<GameObject> Targets ;
		
		public AbilityContextHolder(AbilityDefinition abilityDefinition, GameObject caster, List<GameObject> targets = null, List<IContext> contexts = null) : base(contexts) {
			AbilityDefinition = abilityDefinition;
			Caster = caster;
			Targets = targets ?? new List<GameObject>();
		}
	}
}