using System.Collections.Generic;
using PolyWare.Effects;

namespace PolyWare.Abilities {
	public interface IEffectStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder);
	}
}