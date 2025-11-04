namespace PolyWare.Game {
	public class DefaultFireRateEvaluator : IFireRateEvaluator {
		public float Evaluate(IStatsHandler culprit, float baseFireRate) => baseFireRate;
	}
}