using System.Linq;
using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public class ConeAimAssist : AimAssistStrategy {
		
		public ConeAimAssist(AimAssistInfo aimAssistData, Transform spawnPoint) : base(aimAssistData, spawnPoint) { }
		
		public override void RunAimAssist() {
			Physics.OverlapSphereNonAlloc(spawnPoint.position + spawnPoint.forward * (aimAssistInfo.Range * 0.5f), aimAssistInfo.Radius, aimAssistResults, enemyLayerMask);
			float maxAngle = Mathf.Atan2(aimAssistInfo.Radius, aimAssistInfo.Range) * Mathf.Rad2Deg;
			
			float closestAngle = float.MaxValue;
			Collider bestTarget = null;
			RaycastHit bestHit = default;
			
			foreach (Collider col in aimAssistResults.Where(col => col)) {
				Vector3 toTarget = col.ClosestPoint(spawnPoint.position) - spawnPoint.position;
				float angle = Vector3.Angle(spawnPoint.forward, toTarget);
			
				if (!(angle <= maxAngle)) continue;
				if (!Physics.Raycast(spawnPoint.position, toTarget.normalized, out RaycastHit tempHit, aimAssistInfo.Range, enemyLayerMask)) continue;
				if (tempHit.collider != col || !(angle < closestAngle)) continue;
				
				closestAngle = angle;
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

			if (aimAssistTarget) {
				GizmoHelper.DrawLine(origin, aimAssistHit.point, Color.red, 3);
				GizmoHelper.DrawLine(origin, origin + CalculateDirectionToTarget() * aimAssistInfo.Range, Color.green, 3);
			}
			
			GizmoHelper.DrawCone(origin, spawnPoint.forward, spawnPoint.up, aimAssistInfo.Radius, aimAssistInfo.Range, GizmoHelper.ConeStyle.Line, Color.black, 3);
		}
	}
}