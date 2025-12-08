namespace PolyWare.Game {
	public class StatsHandler : IStatsHandler {
		public readonly StatModifierManager ModifierManager;
		private readonly StatData baseData;
		private readonly StatEvaluatorCollection statEvaluators;

		public StatsHandler(StatModifierManager modifierManager, StatData baseBases, StatEvaluatorCollection statEvaluatorCollection) {
			ModifierManager = modifierManager;
			baseData = baseBases;
			statEvaluators = statEvaluatorCollection;
			statEvaluators?.Initialize();
		}

		public bool AddModifier(StatModifier modifier) {
			ModifierManager.AddModifier(modifier);
			return true;
		}
		
		public float GetScaledStat(CharacterStatType characterStatType) {
			var q = new StatQuery(characterStatType, baseData[characterStatType]);
			ModifierManager.PerformQuery(this, q);
			return statEvaluators && statEvaluators.Contains(characterStatType) ? statEvaluators.Evaluate(characterStatType, q.Value) : q.Value;
		}
		
		public float GetUnscaledStat(CharacterStatType characterStatType) {
			var q = new StatQuery(characterStatType, baseData[characterStatType]);
			ModifierManager.PerformQuery(this, q);
			return q.Value;
		}

		public void Update(float deltaTime) {
			ModifierManager.Update(deltaTime);
		}
	}
}