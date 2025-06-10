using System.Linq;
using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public class ConeAimAssist : AimAssistStrategy {
		protected readonly Collider[] aimAssistResults = new Collider[10];
		
		public ConeAimAssist(AimAssistInfo aimAssistData, Transform spawnPoint) : base(aimAssistData, spawnPoint) { }
		
		public override void RunAimAssist() {
			Physics.OverlapSphereNonAlloc(spawnPoint.position, aimAssistInfo.Range, aimAssistResults, enemyLayerMask);
			float maxAngle = Mathf.Atan2(aimAssistInfo.Radius, aimAssistInfo.Range) * Mathf.Rad2Deg;
			
			float closestAngle = float.MaxValue;
			Collider bestTarget = null;
			RaycastHit bestHit = default;
			
			foreach (Collider col in aimAssistResults.Where(col => col)) {
				Vector3 targetDirection = (col.ClosestPoint(spawnPoint.position) - spawnPoint.position).normalized;

				Vector3 flatToTarget = Vector3.ProjectOnPlane(targetDirection, Vector3.up);

				// Horizontal angle (XZ plane)
				float horizontalAngle = Vector3.Angle(spawnPoint.forward, flatToTarget);
				if (horizontalAngle > maxAngle) continue;
				
				// Vertical angle (Y component relative to horizontal distance)
				if (!CheckVerticalAngle(targetDirection)) continue;

				if (!Physics.Raycast(spawnPoint.position, targetDirection, out RaycastHit tempHit, aimAssistInfo.Range, enemyLayerMask)) continue;
				if (horizontalAngle >= closestAngle) continue;
				
				closestAngle = horizontalAngle;
				bestTarget = col;
				bestHit = tempHit;
			}
			
			if (bestTarget) {
				aimAssistTarget = bestTarget;
				aimAssistHit = bestHit;
			} else {
				aimAssistTarget = null;
			}
		}

		public override void DrawGizmos() {
			Vector3 origin = spawnPoint.position;

			Gizmos.DrawWireSphere(spawnPoint.position, aimAssistInfo.Range);
			
			if (aimAssistTarget) {
				GizmoHelper.DrawLine(origin, aimAssistHit.collider.bounds.center, Color.red, 3);
				GizmoHelper.DrawLine(origin, origin + CalculateAdjustedDirectionToTarget() * aimAssistInfo.Range, Color.green, 3);
			}
			
			GizmoHelper.DrawCone(origin, spawnPoint.forward, spawnPoint.up, aimAssistInfo.Radius, aimAssistInfo.Range, GizmoHelper.ConeStyle.Line, Color.black, 3);
		}
	}
}