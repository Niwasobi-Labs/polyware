using System;
using System.Collections.Generic;
using PolyWare.Characters;
using PolyWare.Effects;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Abilities {
	[Serializable]
	public class EnemiesInRangeActionTargetStrategy : IActionTargetStrategy {
		public float Radius = 5;
		public int MaxTargets = 5;
		
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();
			var results =  new Collider[MaxTargets];

			Vector3 effectOrigin = contextHolder.Culprit.transform.position;
			
			if (contextHolder.Targets.Count > 0) {
				effectOrigin = contextHolder.Targets[0].transform.position;
			}
			
			int hits = Physics.OverlapSphereNonAlloc(effectOrigin, Radius, results, Constants.DamageLayerMask);
			
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