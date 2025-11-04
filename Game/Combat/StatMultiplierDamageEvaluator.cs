using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class StatMultiplierDamageEvaluator : IDamageEvaluator {
		[SerializeField] private StatType stat;
		
		public DamageContext Evaluate(IStatsHandler stats, DamageContext baseDamage) {
			baseDamage.Damage *= stats.GetModifiedStat(stat);
			return baseDamage;
		}
	}
}