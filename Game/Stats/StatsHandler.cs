using System;

namespace PolyWare.Game {
	public class StatsHandler : IStatsHandler {
		public event Action OnStatUpdate;
		
		public readonly StatModifierManager ModifierManager;
		private readonly StatData baseData;
		private readonly StatEvaluatorCollection statEvaluators;

		public StatsHandler(StatModifierManager modifierManager, StatData baseBases, StatEvaluatorCollection statEvaluatorCollection) {
			ModifierManager = modifierManager;
			baseData = baseBases;
			statEvaluators = statEvaluatorCollection;
			statEvaluators?.Initialize();
			ModifierManager.OnModifierListUpdated += RaiseStatUpdateEvent;
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

		private void RaiseStatUpdateEvent() {
			OnStatUpdate?.Invoke();
		}
	}
}