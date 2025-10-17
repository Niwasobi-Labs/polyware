using PolyWare.Combat;

namespace PolyWare.ActionGame {
	public interface IMeleeDamageEvaluator {
		public DamageContext Evaluate(DamageContext baseDamage);
	}
	
	public class DefaultMeleeDamageEvaluator : IMeleeDamageEvaluator {
		public DamageContext Evaluate(DamageContext baseDamage) => baseDamage;
	}
}