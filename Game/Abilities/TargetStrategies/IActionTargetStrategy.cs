using System.Collections.Generic;

namespace PolyWare.Game {
	public interface IActionTargetStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder);
	}
}