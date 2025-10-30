using PolyWare.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class AbilityContextHolder : ContextHolder {
		public AbilityDefinition AbilityDefinition;
		public GameObject Culprit;
		public List<GameObject> Targets;
		
		public AbilityContextHolder(AbilityDefinition abilityDefinition, GameObject culprit, List<GameObject> targets = null, List<IContext> contexts = null) : base(contexts) {
			AbilityDefinition = abilityDefinition;
			Culprit = culprit;
			Targets = targets ?? new List<GameObject>();
		}

		public AbilityContextHolder(AbilityContextHolder other) {
			AbilityDefinition = other.AbilityDefinition;
			Culprit = other.Culprit;
			Targets = new List<GameObject>(other.Targets);
			
			Dictionary<Type, IContext>.ValueCollection otherContexts = other.GetAllContexts();
			foreach (IContext context in otherContexts) {
				Add(context);
			}
		}
	}
}