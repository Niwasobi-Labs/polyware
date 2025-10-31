namespace PolyWare.Game {
	public interface IMaxHealthEvaluator {
		public float Evaluate(IStatsHandler statHandler, HealthData healthData);
	}
}