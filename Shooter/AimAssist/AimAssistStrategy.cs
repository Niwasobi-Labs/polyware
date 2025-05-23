using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public abstract class AimAssistStrategy : IAimAssistStrategy {
		public Collider GetTarget => aimAssistTarget;
		
		protected readonly int enemyLayerMask;
		protected RaycastHit aimAssistHit;
		protected Collider aimAssistTarget;
		protected readonly Collider[] aimAssistResults = new Collider[10];
		
		protected readonly Transform spawnPoint;
		protected readonly AimAssistInfo aimAssistInfo;

		protected AimAssistStrategy(AimAssistInfo aimAssistData, Transform spawnPoint) {
			enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
			aimAssistInfo = aimAssistData;
			this.spawnPoint = spawnPoint;
		}

		public abstract void RunAimAssist();
		
		public Vector3 CalculateDirectionToTarget() {
			if (!aimAssistTarget) return spawnPoint.forward;
			
			Vector3 directionToTarget = Vector3.ProjectOnPlane(aimAssistTarget.ClosestPointOnBounds(spawnPoint.position) - spawnPoint.position, spawnPoint.up).normalized;
			Vector3 gunForward = spawnPoint.forward;
			
			float angle = Vector3.Angle(gunForward, directionToTarget);
			float maxAssistAngle = Mathf.Atan2(aimAssistInfo.Radius, aimAssistInfo.Range) * Mathf.Rad2Deg;
			float assistStrength = aimAssistInfo.Curve.Evaluate(1 - Mathf.Clamp01(angle / maxAssistAngle));
			return Vector3.Slerp(gunForward, directionToTarget, assistStrength);
		}
		
		public virtual void DrawGizmos() { }
	}
}