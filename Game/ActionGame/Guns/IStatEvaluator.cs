namespace PolyWare.Game {
	public interface IStatEvaluator {
		public float Evaluate(IStatsHandler stats, IEffectsHandler effects, float baseValue);
	}
}