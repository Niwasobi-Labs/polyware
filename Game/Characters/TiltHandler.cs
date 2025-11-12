using UnityEngine;

namespace PolyWare.Game {
	public static class TiltHandler {
		public static Quaternion AdjustRotationForTilt_XZ(Rigidbody rb, Quaternion targetRotation, TiltSettings tiltSettings, float deltaTime) {
			// Convert velocity into local space
			Vector3 localVelocity = rb.transform.InverseTransformDirection(rb.linearVelocity);
			float targetPitch = 0;
			float targetRoll = 0;
			
			if (localVelocity.sqrMagnitude > 0.01f) {
				float maxVelocitySqr = rb.maxLinearVelocity * rb.maxLinearVelocity;
				
				float fractionalPitchVelocity = (localVelocity.z * localVelocity.z) / maxVelocitySqr;
				targetPitch = Mathf.LerpAngle(0, tiltSettings.MaxPitch, fractionalPitchVelocity);
				
				float fractionalRollVelocity = (localVelocity.x * localVelocity.x) / maxVelocitySqr;
				targetRoll = Mathf.LerpAngle(0, tiltSettings.MaxRoll, fractionalRollVelocity);
				
				if (localVelocity.z < 0) { // backwards
					targetPitch *= -1;
				}
				
				if (localVelocity.x > 0) { // right
					targetRoll *= -1;
				}
			}
			
			float smoothPitch = Mathf.LerpAngle(rb.transform.rotation.eulerAngles.x, targetPitch, tiltSettings.TiltSpeed * deltaTime);
			float smoothRoll = Mathf.LerpAngle(rb.transform.rotation.eulerAngles.z, targetRoll, tiltSettings.TiltSpeed * deltaTime);
			
			return Quaternion.Euler(smoothPitch, targetRotation.eulerAngles.y, smoothRoll);
		}
	}
}