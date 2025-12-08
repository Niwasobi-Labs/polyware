namespace PolyWare.Game {
	public class NoEvaluationStatEvaluator : IStatEvaluator {
		public float Evaluate(IStatsHandler stats, IEffectsHandler effects, float baseValue) {
			return baseValue;
		}
	}
}