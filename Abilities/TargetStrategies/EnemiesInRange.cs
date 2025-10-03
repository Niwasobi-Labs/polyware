using System;
using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Characters;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[Serializable]
	public class EnemiesInRange : IActionTargetStrategy {
		public float Radius = 5;
		public int MaxTargets = 5;
		
		private const string damageMask = "Damage";
		
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();
			var results =  new Collider[MaxTargets];

			int hits = Physics.OverlapSphereNonAlloc(contextHolder.Culprit.transform.position, Radius, results, LayerMask.GetMask(damageMask));
			
			if (hits == 0) return targets;

			for (int i = 0; i < hits; ++i) {
				if (!results[i]) continue;
				if (!results[i].gameObject.TryGetComponent(out IAffectable affectable)) continue;
				if (contextHolder.Culprit.TryGetComponent(out IFactionMember friendlyFaction) && affectable.GameObject.TryGetComponent(out IFactionMember targetFaction) && friendlyFaction == targetFaction) continue;
				
				targets.Add(affectable);
			}
			
			return targets;
		}
	}
}