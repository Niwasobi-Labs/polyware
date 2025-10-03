using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Core.Entities;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace PolyWare.ActionGame.Projectiles {
	public class ProjectileSpawnStrategy {
		
		public List<Projectile> Spawn(ProjectileContext ctx, Vector3 position, Vector3 forward, float spread, int count, Vector3 spawnUp, AbilityContextHolder abilityCtxHolder = null) {
			return count > 1 ? 
				SpawnMultipleProjectiles(ctx, position, forward, spread, count, spawnUp, abilityCtxHolder) :
				new List<Projectile> { SpawnProjectile(ctx, position, forward, abilityCtxHolder) };
		}

		private List<Projectile> SpawnMultipleProjectiles(ProjectileContext ctx, Vector3 position, Vector3 forward, float spread, int count, Vector3 spawnUp, AbilityContextHolder abilityCtxHolder) {
			var projectiles = new List<Projectile>();
			float angleStep = spread / (count - 1);
			float startingAngle = -(spread / 2f);
			
			for (int i = 0; i < count; i++) {
				float angle = startingAngle + angleStep * i;
				Vector3 newForward = Quaternion.AngleAxis(angle, spawnUp) * forward;
				projectiles.Add(SpawnProjectile(ctx, position, newForward, abilityCtxHolder));
			}

			return projectiles;
		}

		private Projectile SpawnProjectile(ProjectileContext ctx, Vector3 position, Vector3 forward, AbilityContextHolder abilityCtxHolder) {
			var newProjectile = EntityFactory<Projectile>.CreateWith(ctx.Data);
			newProjectile.transform.position = position;
			newProjectile.transform.forward = forward;
			
			if (abilityCtxHolder != null) newProjectile.ProvideAbilityContext(abilityCtxHolder);
			
			return newProjectile;
		}
	}
}