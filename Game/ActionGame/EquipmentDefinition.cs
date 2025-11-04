using UnityEngine;

namespace PolyWare.Game {
	public abstract class EquipmentDefinition : ItemDefinition {

		[field: SerializeField] public MeleeDefinition.MeleeAttackInfo MeleeInfo { get; private set; }

		public DamageContext EvaluateMeleeDamage(IStatsHandler stats) {
			return MeleeInfo.DamageEvaluator.Evaluate(stats, MeleeInfo.Damage);
		}
	}
}