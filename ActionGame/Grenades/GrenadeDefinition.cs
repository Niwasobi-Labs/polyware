using PolyWare.Combat;
using PolyWare.Core.Entities;
using PolyWare.Items;
using UnityEngine;

namespace PolyWare.ActionGame.Grenades {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Grenade", fileName = "New GrenadeDefinition")]
	public class GrenadeDefinition : ItemDefinition {
		public override IEntityData CreateDefaultInstance() => new GrenadeData(this);

		public DamageInfo Damage;
		public float FuseTime = 2.0f;
	}
}