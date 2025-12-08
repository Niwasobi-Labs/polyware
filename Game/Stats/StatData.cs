using System;
using UnityEngine;

namespace PolyWare.Game {
	public enum StatType {
		Strength, 
		Agility,
		Dexterity,
		Vitality,
		Luck,
	}
	
	[Serializable]
	public class StatData {
		[field: SerializeField] public float Strength { get; private set; } = 1;
		[field: SerializeField] public float Agility { get; private set; } = 1;
		[field: SerializeField] public float Dexterity { get; private set; } = 1;
		[field: SerializeField] public float Vitality { get; private set; } = 1;
		[field: SerializeField] public float Damage { get; private set; } = 1;	
		[field: SerializeField] public float Luck { get; private set; } = 1;
		
		public float this[StatType statType] {
			get {
				return statType switch {
					StatType.Strength => Strength,
					StatType.Agility => Agility,
					StatType.Dexterity => Dexterity,
					StatType.Vitality => Vitality,
					StatType.Luck => Luck,
					_ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
				};
			}
		}
	}
}