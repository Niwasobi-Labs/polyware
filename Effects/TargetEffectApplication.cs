using System.Collections.Generic;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	public class TargetEffectStrategy : IEffectStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();

			for (int i = 0; i < contextHolder.AbilityContext.Targets.Count; i++) {
				GameObject target = contextHolder.AbilityContext.Targets[i];

				if (!target.TryGetComponent(out IAffectable affectable)) {
					Log.Error($"Target is not IAffectable {target.name}");
					continue;
				}

				targets.Add(affectable);
			}

			return targets;
		}
	}
}