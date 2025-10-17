namespace PolyWare.Combat {
	public interface IDamageEvaluator {
		public DamageContext Evaluate(DamageContext baseDamage);
	}
}