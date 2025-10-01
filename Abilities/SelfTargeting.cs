using PolyWare.Combat;

namespace PolyWare.Abilities {
	public class SelfTargeting : TargetingStrategy {
		public override void Start(AbilityContext context) {
			context.Ability.Execute(context.Owner.GetComponent<IDamageable>(), context);
		}
	}
}