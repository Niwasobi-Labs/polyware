namespace PolyWare.Game {
	public class StatQuery {
		public readonly CharacterStatType CharacterStatType;
		public float Value;
		
		public StatQuery(CharacterStatType characterStatType, float value) {
			CharacterStatType = characterStatType;
			Value = value;
		}
	}
}