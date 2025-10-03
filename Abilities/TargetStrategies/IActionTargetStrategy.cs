using System.Collections.Generic;
using PolyWare.Effects;

namespace PolyWare.Abilities {
	public interface IActionTargetStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder);
	}
}