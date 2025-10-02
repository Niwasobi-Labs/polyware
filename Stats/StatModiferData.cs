using System;

namespace PolyWare.Stats {
	[Serializable]
	public class StatModiferData {
		public enum OperatorType {
			Add, 
			Multiply
		}

		public StatType StatType = StatType.Attack;
		public OperatorType Type =  OperatorType.Add;
		public float Value = 10f;
		public float Duration = 5f;
	}
}