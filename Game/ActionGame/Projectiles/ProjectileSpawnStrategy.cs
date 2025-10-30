using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class ProjectileSpawnStrategy {
		
		public static List<Projectile> Spawn(ProjectileSpawnContext context) {
			return context.Count > 1 ? 
				SpawnMultipleProjectiles(context) :
				new List<Projectile> { SpawnProjectile(context, context.Direction) };
		}

		private static List<Projectile> SpawnMultipleProjectiles(ProjectileSpawnContext context) {
			var projectiles = new List<Projectile>();
			// ReSharper disable once PossibleLossOfFraction (we are checking inside of Spawn)
			float angleStep = context.Spread / (context.Count - 1);
			float startingAngle = -(context.Spread / 2f);
			
			for (int i = 0; i < context.Count; i++) {
				float angle = startingAngle + angleStep * i;
				projectiles.Add(SpawnProjectile(context, Quaternion.AngleAxis(angle, context.AxisNormal) * context.Direction));
			}

			return projectiles;
		}

		private static Projectile SpawnProjectile(ProjectileSpawnContext context, Vector3 forward) {
			var newProjectile = EntityFactory<Projectile>.CreateWith(context.Projectile);
			newProjectile.transform.position = context.Origin;
			newProjectile.transform.forward = forward;
			
			if (context.AbilityContextHolder != null) newProjectile.ProvideAbilityContext(new AbilityContextHolder(context.AbilityContextHolder));
			
			return newProjectile;
		}
	}
}