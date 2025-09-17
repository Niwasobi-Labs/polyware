namespace PolyWare.Stats {
	public class StatsHandler : IStatsHandler {
		public readonly StatModifierManager ModifierManager;
		private readonly StatData baseData;

		public StatsHandler(StatModifierManager modifierManager, StatData baseBases) {
			ModifierManager = modifierManager;
			baseData = baseBases;
		}

		public bool AddModifier(StatModifier modifier) {
			ModifierManager.AddModifier(modifier);
			return true; // todo: allow for future modifiers to have add conditions
		}
		
		public float GetModifiedStat(StatType statType) {
			var q = new StatQuery(statType, baseData[statType]);
			ModifierManager.PerformQuery(this, q);
			return q.Value;
		}

		public void Update(float deltaTime) {
			ModifierManager.Update(deltaTime);
		}
	}
}