using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[CreateAssetMenu(fileName = "New Projectile Definition", menuName = "PolyWare/Projectile")]
	public class ProjectileDefinition : EntityDefinition {
		[Header("Magnetism")] [Range(0, 1)]
		[field: SerializeField] public float MagnetismStrength { get; private set; }
		[field: SerializeField] public bool AllowSelfDamage { get; private set; }
		
		public override IEntityData CreateDefaultInstance() => new ProjectileData(this);
	}
}