using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public struct ProjectileSpawnContext : IContext {
		public readonly ProjectileData Projectile;
		public readonly int Count;
		public readonly Vector3 Origin;
		public readonly Vector3 Direction;
		public readonly Vector3 AxisNormal;
		public readonly float Spread;
		public readonly AbilityContextHolder AbilityContextHolder;
		
		public ProjectileSpawnContext(ProjectileData projectile, int count, Vector3 origin, Vector3 direction, Vector3 axisNormal, float spread, AbilityContextHolder abilityContextHolder = null) {
			Projectile = projectile;
			Count = count;
			Origin = origin;
			Direction = direction;
			AxisNormal = axisNormal;
			Spread = spread;
			AbilityContextHolder = abilityContextHolder;
		}
	}
}