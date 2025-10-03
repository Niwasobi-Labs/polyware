using System.Collections.Generic;
using PolyWare.Debug;
using PolyWare.Effects;

namespace PolyWare.Abilities {
	public class SelfEffectStrategy : IEffectStrategy {
		public List<IAffectable> GetTargets(AbilityContextHolder contextHolder) {
			if (contextHolder.Caster.TryGetComponent(out IAffectable affectable)) return new List<IAffectable> { affectable };
			
			Log.Error($"Owner is not IAffectable {contextHolder.Caster.name}");
			return new List<IAffectable>();
		}
	}
}