using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public struct HealthData {
		public float InitialMaxHealth;
		[SerializeReference] private IMaxHealthEvaluator maxHealthEvaluator;
		[ReadOnly] public float Current;
		public bool Invincible;
		public bool CanHeal;
			
		public bool CanRegen;
		[ShowIf("CanRegen")] public float RegenDelayTime;
		[ShowIf("CanRegen")] public float RegenTime;

		public float MaxHealth(ICharacter character = null) {
			return maxHealthEvaluator?.Evaluate(character, this) ?? InitialMaxHealth;
		}
	}
}