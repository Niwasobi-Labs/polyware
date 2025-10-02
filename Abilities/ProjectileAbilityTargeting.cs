using PolyWare.ActionGame.Projectiles;
using PolyWare.Core;
using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.Abilities {
	public class ProjectileAbilityTargeting : AbilityTargetingStrategy {
		private ProjectileContext projectileContext;
		private AbilityContextHolder abilityContextHolder;
		
		public override void Start(AbilityContextHolder contextHolder) {
			abilityContextHolder = contextHolder;
			projectileContext = contextHolder.Get<ProjectileContext>();
			
			if (projectileContext.SpawnCount > 1) {
				SpawnMultipleProjectiles(); // todo: move this back to something else (ProjectileSpawnStrategy)
			}
			else {
				SpawnProjectile(projectileContext.Data, abilityContextHolder.AbilityContext.Position, projectileContext.Data.StartingDirection);
			}
		}

		private void SpawnMultipleProjectiles() {
			float angleStep = projectileContext.Spread / (projectileContext.SpawnCount - 1);
			float startingAngle = -(projectileContext.Spread / 2f);
			
			for (int i = 0; i < projectileContext.SpawnCount; i++) {
				float angle = startingAngle + angleStep * i;
				Vector3 forward = Quaternion.AngleAxis(angle, projectileContext.SpawnUp) * projectileContext.Data.StartingDirection;
				SpawnProjectile(projectileContext.Data, abilityContextHolder.AbilityContext.Position, forward);
			}
		}
		
		private void SpawnProjectile(ProjectileData projectile, Vector3 position, Vector3 forward) {
			var newProjectile = EntityFactory<Projectile>.CreateWith(projectile);
			newProjectile.transform.position = position;
			newProjectile.transform.forward = forward;
			newProjectile.ProvideContext(abilityContextHolder);
		}
	}
}