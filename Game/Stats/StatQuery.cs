namespace PolyWare.Stats {
	public class StatQuery {
		public readonly StatType StatType;
		public float Value;
		
		public StatQuery(StatType statType, float value) {
			StatType = statType;
			Value = value;
		}
	}
}