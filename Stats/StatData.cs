using System;

namespace PolyWare.Stats {
	public enum StatType {
		Strength, 
		Speed,
		Dexterity,
		Vitality,
	}
	
	[Serializable]
	public class StatData {
		public float Strength = 1;
		public float Speed = 1;
		public float Dexterity = 1;
		public float Vitality = 1;
		
		public float this[StatType statType] {
			get {
				return statType switch {
					StatType.Strength => Strength,
					StatType.Speed => Speed,
					StatType.Dexterity => Dexterity,
					StatType.Vitality => Vitality,
					_ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
				};
			}
		}
	}
}