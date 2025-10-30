using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class StatMultiplierDamageEvaluator : IDamageEvaluator {
		[SerializeField] private StatType stat;
		
		public DamageContext Evaluate(DamageContext baseDamage) {
			if (baseDamage.Culprit && baseDamage.Culprit.TryGetComponent(out ICharacter character)) {
				baseDamage.Damage *= character.Stats.GetModifiedStat(stat);
			}
			return baseDamage;
		}
	}
}