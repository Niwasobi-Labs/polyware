namespace PolyWare.Game {
	public class DefaultMaxHealthEvaluator : IMaxHealthEvaluator {
		public float Evaluate(IStatsHandler statHandler, HealthData healthData) {
			if (statHandler != null) return healthData.InitialMaxHealth + statHandler.GetModifiedStat(StatType.Vitality);
			return healthData.InitialMaxHealth;
		}
	}
}