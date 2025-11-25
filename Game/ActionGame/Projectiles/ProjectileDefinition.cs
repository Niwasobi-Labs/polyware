using FMODUnity;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Projectile", fileName = "New ProjectileDefinition")]
	public class ProjectileDefinition : EntityDefinition {
		[Header("Magnetism")] [Range(0, 1)]
		[field: SerializeField] public float MagnetismStrength { get; private set; }
		[field: SerializeField] public bool AllowSelfDamage { get; private set; }
		[field: SerializeField] public EventReference ImpactSound { get; private set; }
		[field: SerializeField] public GameObject ImpactVFX { get; private set; }
		public override IEntityData CreateDefaultInstance() => new ProjectileData(this);
	}
}