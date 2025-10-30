using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class TargetActionTargetStrategy : IActionTargetStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();

			for (int i = 0; i < contextHolder.Targets.Count; i++) {
				GameObject target = contextHolder.Targets[i];

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