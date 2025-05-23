using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public class SphereCastAimAssist : AimAssistStrategy {
		public SphereCastAimAssist(AimAssistInfo aimAssistInfo, Transform spawnPoint) : base(aimAssistInfo, spawnPoint) { }
		
		public override void RunAimAssist() {
			
			if (Physics.SphereCast(spawnPoint.position, aimAssistInfo.Radius, spawnPoint.forward, out RaycastHit hit, aimAssistInfo.Range, enemyLayerMask)) {
				aimAssistTarget = hit.collider;
				aimAssistHit = hit;
			}
			else {
				aimAssistTarget = null;
			}
		}

		public override void DrawGizmos() {
			
			Vector3 origin = spawnPoint.position;
			Vector3 direction = spawnPoint.forward.normalized;

			if (aimAssistTarget) {
				GizmoHelper.DrawLine(origin, aimAssistHit.point, Color.red, 2);
				GizmoHelper.DrawLine(origin, origin + spawnPoint.forward * aimAssistInfo.Range, Color.green, 2);
				GizmoHelper.DrawLine(origin, origin + CalculateDirectionToTarget() * aimAssistInfo.Range, Color.cyan, 2);
			}
			
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(origin, aimAssistInfo.Radius);
			GizmoHelper.DrawLine(origin + spawnPoint.up * aimAssistInfo.Radius, origin + spawnPoint.up * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + -spawnPoint.up * aimAssistInfo.Radius, origin + -spawnPoint.up * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + spawnPoint.right * aimAssistInfo.Radius, origin + spawnPoint.right * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + -spawnPoint.right * aimAssistInfo.Radius, origin + -spawnPoint.right * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			Gizmos.DrawWireSphere(origin + direction * aimAssistInfo.Range, aimAssistInfo.Radius);
		}
	}
}