using UnityEngine;

namespace PolyWare.Game {
	public enum AimAssistMode {
		None,
		SphereCast,
		ConeCast
	}
	
	public abstract class AimAssistStrategy {
		protected readonly int enemyLayerMask;
		protected IAimAssistTarget aimAssistTarget;
		
		protected readonly Transform spawnPoint;
		protected readonly AimAssistInfo aimAssistInfo;

		public static AimAssistStrategy Create(AimAssistInfo aimAssist, Transform aimAssistSpawnPoint) {
			return aimAssist.Mode switch {
				AimAssistMode.SphereCast => new SphereCastAimAssist(aimAssist, aimAssistSpawnPoint),
				AimAssistMode.ConeCast => new ConeAimAssist(aimAssist, aimAssistSpawnPoint),
				_ => new NoAimAssist(aimAssist, aimAssistSpawnPoint)
			};
		}
		
		protected AimAssistStrategy(AimAssistInfo aimAssistData, Transform spawnPoint) {
			enemyLayerMask = aimAssistData.LayerMask;
			aimAssistInfo = aimAssistData;
			this.spawnPoint = spawnPoint;
		}

		protected static bool TryFilterTarget(Collider collider, out IAimAssistTarget target) {
			return collider.TryGetComponent(out target);
		}
		
		public abstract void RunAimAssist();
		
		protected bool CheckVerticalAngle(Vector3 targetDirection) {
			float verticalAngle = Mathf.Atan2(targetDirection.y, new Vector2(targetDirection.x, targetDirection.z).magnitude) * Mathf.Rad2Deg;
			return Mathf.Abs(verticalAngle) <= AimAssistInfo.DefaultMaxVerticalAngle;
		}
		
		public Vector3 CalculateAdjustedDirectionToTarget() {
			if (aimAssistTarget == null) return spawnPoint.forward;

			Vector3 toTarget = aimAssistTarget.AimPoint.position - spawnPoint.position;
			Vector3 flatToTarget = Vector3.ProjectOnPlane(toTarget, spawnPoint.up).normalized;
			Vector3 gunForward = spawnPoint.forward;
			
			float angle = Vector3.Angle(gunForward, flatToTarget);
			float maxAssistAngle = Mathf.Atan2(aimAssistInfo.Radius, aimAssistInfo.Range) * Mathf.Rad2Deg;
			float assistStrength = aimAssistInfo.Curve.Evaluate(1 - Mathf.Clamp01(angle / maxAssistAngle));
			
			Vector3 flatDirection = Vector3.Slerp(gunForward, flatToTarget, assistStrength);
			
			Vector3 verticalNormal = Vector3.Cross(flatDirection, Vector3.up);
			
			return Vector3.ProjectOnPlane(toTarget, verticalNormal).normalized;
		}

		public Transform GetTargetTransform() {
			return aimAssistTarget?.AimPoint;	
		}
		
		public virtual void DrawGizmos() { }
	}
}