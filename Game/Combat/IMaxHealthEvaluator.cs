using PolyWare.Characters;

namespace PolyWare.Combat {
	public interface IMaxHealthEvaluator {
		public float Evaluate(ICharacter character, HealthData healthData);
	}
}