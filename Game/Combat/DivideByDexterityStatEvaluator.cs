namespace PolyWare.Game {
	public class DivideByDexterityStatEvaluator : IStatEvaluator {
		public float Evaluate(IStatsHandler stats, IEffectsHandler effects, float baseValue) {
			if (stats == null || effects == null) return baseValue;
			
			return baseValue / stats.GetScaledStat(CharacterStatType.Dexterity);
		}
	}
} 