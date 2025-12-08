using UnityEngine;

namespace PolyWare.Game {
	public abstract class EquipmentDefinition : ItemDefinition {

		[field: SerializeField] public MeleeDefinition.MeleeAttackInfo MeleeInfo { get; private set; }

		public float EvaluateMeleeDamage(IStatsHandler stats, IEffectsHandler effects = null) {
			return MeleeInfo.DamageEvaluator.Evaluate(stats, effects, MeleeInfo.Damage.Damage);
		}
	}
}