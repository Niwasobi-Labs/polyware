using System;

namespace PolyWare.Stats {
	public enum StatType {
		Attack, 
		Speed
	}
	
	[Serializable]
	public class StatData {
		public float Attack = 1;
		public float Speed = 1;
		
		public float this[StatType statType] {
			get {
				return statType switch {
					StatType.Attack => Attack,
					StatType.Speed => Speed,
					_ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
				};
			}
		}
	}
}