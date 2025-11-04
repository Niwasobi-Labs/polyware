namespace PolyWare.Game {
	public class DefaultDamageEvaluator : IDamageEvaluator {
		public DamageContext Evaluate(IStatsHandler stats, DamageContext baseDamage) => baseDamage;
	}
}