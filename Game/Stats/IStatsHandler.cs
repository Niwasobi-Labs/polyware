namespace PolyWare.Game {
	public interface IStatsHandler {
		public bool AddModifier(StatModifier modifier);
		public float GetScaledStat(CharacterStatType types);
		public float GetUnscaledStat(CharacterStatType types);
		public void Update(float deltaTime);
	}
}