namespace PolyWare.Game {
	public interface IStatsHandler {
		public bool AddModifier(StatModifier modifier);
		public float GetModifiedStat(StatType type);
		public void Update(float deltaTime);
	}
}