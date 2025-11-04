namespace PolyWare.Game {
	public class StatMultiplierFireRateEvaluator : IFireRateEvaluator {
		public StatType Stat;

		public float Evaluate(IStatsHandler stats, float baseFireRate) {
			if (stats != null) {
				baseFireRate /= stats.GetModifiedStat(Stat);
			}
			
			return baseFireRate;
		}
	}
}