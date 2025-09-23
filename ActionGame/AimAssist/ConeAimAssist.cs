using System.Linq;
using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.ActionGame.AimAssist {
	public class ConeAimAssist : AimAssistStrategy {
		protected readonly Collider[] aimAssistResults = new Collider[10];
		
		public ConeAimAssist(AimAssistInfo aimAssistData, Transform spawnPoint) : base(aimAssistData, spawnPoint) { }
		
		public override void RunAimAssist() {
			int hits = Physics.OverlapSphereNonAlloc(spawnPoint.position, aimAssistInfo.Range, aimAssistResults, enemyLayerMask);
			if (hits == 0) {
				aimAssistTarget = null;
				return;
			}
			
			float maxAngle = Mathf.Atan2(aimAssistInfo.Radius, aimAssistInfo.Range) * Mathf.Rad2Deg;
			
			float closestAngle = float.MaxValue;
			IAimAssistTarget bestTarget = null;
			
			foreach (Collider col in aimAssistResults.Where(col => col)) {
				if (!TryFilterTarget(col, out IAimAssistTarget newTarget)) continue;
				
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
				bestTarget = newTarget;
			}

			aimAssistTarget = bestTarget;
		}

		public override void DrawGizmos() {
			Vector3 origin = spawnPoint.position;

			Gizmos.DrawWireSphere(spawnPoint.position, aimAssistInfo.Range);
			
			if (aimAssistTarget != null) {
				GizmoHelper.DrawLine(origin, aimAssistTarget.AimPoint.position, Color.red, 3);
				GizmoHelper.DrawLine(origin, origin + CalculateAdjustedDirectionToTarget() * aimAssistInfo.Range, Color.green, 3);
			}
			
			GizmoHelper.DrawCone(origin, spawnPoint.forward, spawnPoint.up, aimAssistInfo.Radius, aimAssistInfo.Range, GizmoHelper.ConeStyle.Line, Color.black, 3);
		}
	}
}