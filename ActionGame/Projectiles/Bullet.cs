using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	public class Bullet : Projectile {
		
		private void FixedUpdate() {
			if (target) transform.forward = CalculateMagnetizedDirection(); 
			 
			transform.position += transform.forward * (speed * Time.fixedDeltaTime);
		}
		
		private Vector3 CalculateMagnetizedDirection() {
			Vector3 targetDir = target.position - transform.position;
			return Vector3.Slerp(transform.forward, targetDir, projectileData.MagnetismStrength * Time.fixedDeltaTime).normalized;
		} 
		
		private void OnDrawGizmos() {
			if (!target) return;
			
			GizmoHelper.DrawLine(transform.position, target.position, Color.magenta, 5);
			GizmoHelper.DrawLine(transform.position, transform.position + transform.forward * 10f, Color.white, 5);
		}
	}
}