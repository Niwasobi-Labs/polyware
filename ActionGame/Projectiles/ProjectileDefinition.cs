using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[CreateAssetMenu(fileName = "New Projectile Definition", menuName = "PolyWare/Projectile")]
	public class ProjectileDefinition : ScriptableObject {
		public GameObject Prefab;
		[Header("Magnetism")] [Range(0, 1)]
		[field: SerializeField] public float MagnetismStrength { get; private set; }
	}
}