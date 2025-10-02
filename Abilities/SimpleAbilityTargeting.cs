using PolyWare.Combat;
using PolyWare.Core;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	public class SimpleAbilityTargeting : AbilityTargetingStrategy {
		
		public override void Start(AbilityContextHolder contextHolder) {
			for (int i = 0; i < contextHolder.AbilityContext.Targets.Count; i++) {
				GameObject target = contextHolder.AbilityContext.Targets[i];
				
				if (!target.TryGetComponent(out IAffectable affectable)) {
					Log.Error($"Target is not IAffectable {target.name}");
					continue;
				}
				
				contextHolder.AbilityContext.Ability.Execute(affectable, contextHolder);
			}
		}
	}
}