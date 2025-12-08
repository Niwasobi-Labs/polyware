namespace PolyWare.Game {
	public class DefaultMaxHealthEvaluator : IStatEvaluator {
		public float Evaluate(IStatsHandler statHandler, IEffectsHandler effects, float maxHealth) {
			return maxHealth;
		}
	}
}