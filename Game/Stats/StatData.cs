using System;
using UnityEngine;

namespace PolyWare.Game {
	public enum StatType {
		Strength, 
		Speed,
		Dexterity,
		Vitality,
		Damage,
		Luck,
	}
	
	[Serializable]
	public class StatData {
		[field: SerializeField] public float Strength { get; private set; } = 1;
		[field: SerializeField] public float Speed { get; private set; } = 1;
		[field: SerializeField] public float Dexterity { get; private set; } = 1;
		[field: SerializeField] public float Vitality { get; private set; } = 1;
		[field: SerializeField] public float Damage { get; private set; } = 1;	
		[field: SerializeField] public float Luck { get; private set; } = 1;
		
		public float this[StatType statType] {
			get {
				return statType switch {
					StatType.Strength => Strength,
					StatType.Speed => Speed,
					StatType.Dexterity => Dexterity,
					StatType.Vitality => Vitality,
					StatType.Damage => Damage,
					StatType.Luck => Luck,
					_ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
				};
			}
		}
	}
}