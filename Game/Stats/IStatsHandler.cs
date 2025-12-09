using System;

namespace PolyWare.Game {
	public interface IStatsHandler {
		public event Action OnStatUpdate;
		public bool AddModifier(StatModifier modifier);
		public float GetScaledStat(CharacterStatType types);
		public float GetUnscaledStat(CharacterStatType types);
		public void Update(float deltaTime);
	}
}