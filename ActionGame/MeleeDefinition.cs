using System;
using PolyWare.Combat;
using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.ActionGame {
	[CreateAssetMenu(fileName = "New Melee Equipment", menuName = "PolyWare/Item/Melee Equipment")]
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