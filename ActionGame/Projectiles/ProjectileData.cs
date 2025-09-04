using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[CreateAssetMenu(fileName = "Projectile", menuName = "PolyWare/Projectiles/Projectile")]
	public class ProjectileData : ScriptableObject {
		[Header("Magnetism")] [Range(0, 1)]
		[field: SerializeField] public float MagnetismStrength { get; private set; }
	}
}