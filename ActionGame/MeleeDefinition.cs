using System;
using PolyWare.Combat;
using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.ActionGame {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Melee", fileName = "New MeleeDefinition")]
	public class MeleeDefinition : EquipmentDefinition {
		public override IEntityData CreateDefaultInstance() => new MeleeData(this);
		[Serializable]
		public struct MeleeAttackInfo {
			public float Cooldown;
			public DamageInfo Damage;
			public AudioClip Sound;
			public float Range;
		}
	}
}