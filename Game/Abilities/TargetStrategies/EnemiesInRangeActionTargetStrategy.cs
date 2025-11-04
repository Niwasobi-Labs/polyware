using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
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
				if (contextHolder.TryGet(out FactionContext factionContext) && affectable.GameObject.TryGetComponent(out IFactionMember targetFaction) && !FactionSystem.CanDamageEachOther(factionContext, targetFaction.FactionInfo)) continue;
				
				targets.Add(affectable);
			}
			
			return targets;
		}
	}
}