using PolyWare.Combat;
using PolyWare.Core;
using PolyWare.Debug;
using PolyWare.Effects;

namespace PolyWare.Abilities {
	public class SelfAbilityTargeting : AbilityTargetingStrategy {
		public override void Start(AbilityContextHolder contextHolder) {

			if (!contextHolder.AbilityContext.Owner.TryGetComponent(out IAffectable affectable)) {
				Log.Error($"Owner is not IAffectable {contextHolder.AbilityContext.Owner.name}");
				return;
			}
			
			contextHolder.AbilityContext.Ability.Execute(affectable, contextHolder);
		}
	}
}