namespace PolyWare.Game {
	public interface IDamageEvaluator {
		public DamageContext Evaluate(DamageContext baseDamage);
	}
}