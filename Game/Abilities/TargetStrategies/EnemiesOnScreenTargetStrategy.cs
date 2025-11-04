using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class EnemiesOnScreenTargetStrategy : IActionTargetStrategy {
		private const string damageMask = "Damage";
		public readonly int MaxTargets = 1024;
		private CastingBox currentCastingBox;
		private Collider[] results;
		
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();
			
			results = new  Collider[MaxTargets];
			currentCastingBox.Rebuild();
			int hits = Physics.OverlapBoxNonAlloc(currentCastingBox.Center, currentCastingBox.HalfExtents, results, currentCastingBox.Rotation, Constants.DamageLayerMask);

			for (int i = 0; i < hits; ++i) {
				if (!results[i]) continue;
				if (!results[i].TryGetComponent(out IAffectable affectable)) continue;
				if (contextHolder.TryGet(out FactionContext factionContext) && affectable.GameObject.TryGetComponent(out IFactionMember targetFaction) && !FactionSystem.CanDamageEachOther(factionContext, targetFaction.FactionInfo)) continue;
				targets.Add(affectable);
			}
			
			return targets;
		}
	}
}