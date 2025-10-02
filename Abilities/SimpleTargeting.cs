using PolyWare.Combat;

namespace PolyWare.Abilities {
	public class SimpleTargeting : TargetingStrategy {
		public override void Start(AbilityContext context) {
			for (int i = 0; i < context.Targets.Count; i++) {
				context.Ability.Execute(context.Targets[i].GetComponent<IDamageable>(), context);
			}
		}
	}
}