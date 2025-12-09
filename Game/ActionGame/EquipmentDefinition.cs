using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	public abstract class EquipmentDefinition : ItemDefinition {

		[Title("Weapon Attributes")] 
		[SerializeReference] public IEffectFactory[] OnEquipEffects;
		
		[field: SerializeField] public MeleeDefinition.MeleeAttackInfo MeleeInfo { get; private set; }

		public float EvaluateMeleeDamage(IStatsHandler stats, IEffectsHandler effects = null) {
			return MeleeInfo.DamageEvaluator.Evaluate(stats, effects, MeleeInfo.Damage.Damage);
		}
	}
}