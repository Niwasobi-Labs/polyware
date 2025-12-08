using System;
using UnityEngine;

namespace PolyWare.Game {
	// don't change these numbers, it'll break everything
	public enum CharacterStatType {
		None = 0,
		Strength = 1, 
		Agility = 2,
		Dexterity = 3,
		Vitality = 4,
		Luck = 5,
		Constitution = 6,
	}
	
	[Serializable]
	public class StatData {
		[field: SerializeField] public float Strength { get; private set; } = 1;
		[field: SerializeField] public float Agility { get; private set; } = 1;
		[field: SerializeField] public float Dexterity { get; private set; } = 1;
		[field: SerializeField] public float Vitality { get; private set; } = 1;
		[field: SerializeField] public float Damage { get; private set; } = 1;	
		[field: SerializeField] public float Luck { get; private set; } = 1;
		[field: SerializeField] public float Constitution { get; private set; } = 1;
		
		public float this[CharacterStatType characterStatType] {
			get {
				return characterStatType switch {
					CharacterStatType.Strength => Strength,
					CharacterStatType.Agility => Agility,
					CharacterStatType.Dexterity => Dexterity,
					CharacterStatType.Vitality => Vitality,
					CharacterStatType.Luck => Luck,
					CharacterStatType.Constitution => Constitution,
					_ => throw new ArgumentOutOfRangeException(nameof(characterStatType), characterStatType, null)
				};
			}
		}
	}
}