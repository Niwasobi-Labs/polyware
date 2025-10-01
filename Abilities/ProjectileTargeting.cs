using PolyWare.ActionGame.Projectiles;
using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.Abilities {
	public class ProjectileTargeting : TargetingStrategy {
		private AbilityContext context;
		private ProjectileContext projectileContext;
		
		public override void Start(AbilityContext ctx) {
			context = ctx;
			projectileContext = context.Get<ProjectileContext>();
			
			if (projectileContext.SpawnCount > 1) {
				SpawnMultipleProjectiles();
			}
			else {
				SpawnProjectile(projectileContext.Data, context.Position, projectileContext.Data.StartingDirection);
			}
		}

		private void SpawnMultipleProjectiles() {
			float angleStep = projectileContext.Spread / (projectileContext.SpawnCount - 1);
			float startingAngle = -(projectileContext.Spread / 2f);
			
			for (int i = 0; i < projectileContext.SpawnCount; i++) {
				float angle = startingAngle + angleStep * i;
				Vector3 forward = Quaternion.AngleAxis(angle, projectileContext.SpawnUp) * projectileContext.Data.StartingDirection;
				SpawnProjectile(context.Get<ProjectileContext>().Data, context.Position, forward);
			}
		}
		
		private void SpawnProjectile(ProjectileData projectile, Vector3 position, Vector3 forward) {
			var newProjectile = EntityFactory<Projectile>.CreateWith(projectile);
			newProjectile.transform.position = position;
			newProjectile.transform.forward = forward;
		}
	}
}