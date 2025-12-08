using System;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Melee", fileName = "New MeleeDefinition")]
	public class MeleeDefinition : EquipmentDefinition {
		public override IEntityData CreateDefaultInstance() => new MeleeData(this);
		
		[Serializable]
		public class MeleeAttackInfo { // instance is found in Equipment Definition (all equipment can melee)
			public AbilityDefinition MeleeAbility;
			public float Cooldown;
			public DamageContext Damage;
			[SerializeReference] public IStatEvaluator DamageEvaluator;
			public float Range;
		}
	}
}