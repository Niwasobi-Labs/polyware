using System;

namespace PolyWare.Game {
	[Serializable]
	public class StatModiferData {
		public enum OperatorType {
			Add, 
			Multiply
		}

		public CharacterStatType CharacterStatType = CharacterStatType.Strength;
		public OperatorType Type =  OperatorType.Add;
		public float Value = 10f;
		public float Duration = 5f;
	}
}