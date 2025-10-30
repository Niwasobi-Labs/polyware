using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Grenade", fileName = "New GrenadeDefinition")]
	public class GrenadeDefinition : ItemDefinition {
		public override IEntityData CreateDefaultInstance() => new GrenadeData(this);

		public DamageContext Damage;
		public float FuseTime = 2.0f;
	}
}