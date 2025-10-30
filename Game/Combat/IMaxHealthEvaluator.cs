namespace PolyWare.Game {
	public interface IMaxHealthEvaluator {
		public float Evaluate(ICharacter character, HealthData healthData);
	}
}