namespace PolyWare.Game {
	public class DefaultMaxHealthEvaluator : IMaxHealthEvaluator {
		public float Evaluate(ICharacter character, HealthData healthData) {
			if (character != null) return healthData.InitialMaxHealth + character.Stats.GetModifiedStat(StatType.Vitality);
			return healthData.InitialMaxHealth;
		}
	}
}