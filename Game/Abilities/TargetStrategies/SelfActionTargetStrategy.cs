using System.Collections.Generic;
using PolyWare.Core;

namespace PolyWare.Game {
	public class SelfActionTargetStrategy : IActionTargetStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			if (contextHolder.Culprit.TryGetComponent(out IAffectable affectable)) return new List<IAffectable> { affectable };
			
			Log.Error($"Owner is not IAffectable {contextHolder.Culprit.name}");
			return new List<IAffectable>();
		}
	}
}