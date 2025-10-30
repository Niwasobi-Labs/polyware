using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class StraightProjectileMovementHandler : ProjectileMovementHandler {
		public override void Move() {
			if (projectile.Data.Target) projectile.transform.forward = CalculateMagnetizedDirection(); 
			 
			projectile.transform.position += projectile.transform.forward * (projectile.Data.Speed * Time.fixedDeltaTime);
		}
		
		private Vector3 CalculateMagnetizedDirection() {
			Vector3 targetDir = projectile.Data.Target.position - projectile.transform.position;
			return Vector3.Slerp(projectile.transform.forward, targetDir, projectile.Data.Definition.MagnetismStrength * Time.fixedDeltaTime).normalized;
		} 
		
		private void OnDrawGizmos() {
			if (!Application.isPlaying || !projectile.Data.Target) return;
			
			GizmoHelper.DrawLine(transform.position, projectile.Data.Target.position, Color.magenta, 5);
			GizmoHelper.DrawLine(projectile.transform.position, projectile.transform.position + projectile.transform.forward * 10f, Color.white, 5);
		}
	}
}