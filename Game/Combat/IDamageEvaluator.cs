namespace PolyWare.Game {
	public interface IDamageEvaluator {
		public DamageContext Evaluate(IStatsHandler stats, DamageContext baseDamage);
	}
}