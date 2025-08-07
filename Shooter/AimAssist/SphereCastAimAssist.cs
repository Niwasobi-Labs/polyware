using System.Linq;
using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public class SphereCastAimAssist : AimAssistStrategy {
		protected readonly RaycastHit[] aimAssistResults = new RaycastHit[10];
		
		public SphereCastAimAssist(AimAssistInfo aimAssistInfo, Transform spawnPoint) : base(aimAssistInfo, spawnPoint) { }
		
		public override void RunAimAssist() {

			Physics.SphereCastNonAlloc(spawnPoint.position + spawnPoint.forward * (aimAssistInfo.Radius * 0.5f), aimAssistInfo.Radius, spawnPoint.forward, aimAssistResults, aimAssistInfo.Range, enemyLayerMask);
			
			float closest = float.MaxValue;
			IAimAssistTarget bestTarget = null;

			foreach (RaycastHit hit in aimAssistResults.Where(hit => hit.collider)) {
				if (!TryFilterTarget(hit.collider, out IAimAssistTarget newTarget)) continue;
				
				Vector3 toTarget = (hit.collider.ClosestPoint(spawnPoint.position) - spawnPoint.position);
				
				// Vertical angle (Y component relative to horizontal distance)
				if (!CheckVerticalAngle(toTarget)) continue;

				if (!Physics.Raycast(spawnPoint.position, toTarget.normalized, out RaycastHit tempHit, aimAssistInfo.Range, enemyLayerMask)) continue;

				float magnitude = toTarget.magnitude;
				if (closest < toTarget.sqrMagnitude) continue;
				
				closest = toTarget.sqrMagnitude;
				bestTarget = newTarget;
			}

			aimAssistTarget = bestTarget;
		}

		public override void DrawGizmos() {
			
			Vector3 origin = spawnPoint.position;
			Vector3 direction = spawnPoint.forward.normalized;

			if (aimAssistTarget != null) {
				GizmoHelper.DrawLine(origin, aimAssistTarget.AimPoint.position, Color.red, 2);
				GizmoHelper.DrawLine(origin, origin + spawnPoint.forward * aimAssistInfo.Range, Color.green, 2);
				GizmoHelper.DrawLine(origin, origin + CalculateAdjustedDirectionToTarget() * aimAssistInfo.Range, Color.cyan, 2);
			}
			
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(spawnPoint.position + spawnPoint.forward * (aimAssistInfo.Radius * 0.5f), aimAssistInfo.Radius);
			GizmoHelper.DrawLine(origin + spawnPoint.up * aimAssistInfo.Radius, origin + spawnPoint.up * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + -spawnPoint.up * aimAssistInfo.Radius, origin + -spawnPoint.up * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + spawnPoint.right * aimAssistInfo.Radius, origin + spawnPoint.right * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			GizmoHelper.DrawLine(origin + -spawnPoint.right * aimAssistInfo.Radius, origin + -spawnPoint.right * aimAssistInfo.Radius + direction * aimAssistInfo.Range, Color.yellow, 2);
			Gizmos.DrawWireSphere(origin + direction * (aimAssistInfo.Range - aimAssistInfo.Radius * 0.5f), aimAssistInfo.Radius);
		}
	}
}