using System.Collections.Generic;

namespace PolyWare.Game {
	public class SelfActionTargetStrategy : IActionTargetStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			var targets = new List<IAffectable>();
			
			if (contextHolder.Culprit && contextHolder.Culprit.TryGetComponent(out IAffectable affectable)) targets.Add(affectable);
			
			return targets;
		}
	}
}