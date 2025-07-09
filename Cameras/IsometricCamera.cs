using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Cameras {
	public class IsometricCamera : MonoBehaviour {
		[field: Title("Camera")]
		[field: SerializeField] public Camera CameraRef { get; private set;  }

		[Title("Movement Settings")]
		[ReadOnly] private Transform target;
		[SerializeField] private float smoothTime = 20f;

		[Title("Aiming Settings")]
		[SerializeField]  private float aimingLead = 5f;
		[SerializeField] private float aimingSmoothTime = 20f;
		
		private Vector3 currentAimingOffset = Vector3.zero;
		private Vector3 targetAimingOffset = Vector3.zero;
		
		private bool isAiming;
		private bool isPaused;
		
		private Vector3 leadVelocity = Vector3.zero;
		private Vector3 moveVelocity = Vector3.zero;

		private void FixedUpdate() {
			if (isPaused || !target) return;

			UpdateAimingOffset();

			transform.position = Vector3.SmoothDamp(transform.position, CalculateNewPos(), ref moveVelocity, smoothTime * Time.fixedDeltaTime);
		}

		public void SetTarget(Transform newTarget) {
			target = newTarget;
		}

		public void Pause() {
			isPaused = true;
		}

		public void Unpause() {
			isPaused = false;
			transform.position = CalculateTargetPos();
		}

		public void ToggleAimingMode(bool status, Vector3 aimingDirection) {
			isAiming = status;

			if (isAiming)  targetAimingOffset = aimingDirection.normalized * aimingLead;
			else  targetAimingOffset = Vector3.zero;
		}

		private Vector3 CalculateTargetPos() {
			return target.position + targetAimingOffset;
		}

		private Vector3 CalculateNewPos() {
			return target.position + currentAimingOffset;
		}

		private void UpdateAimingOffset() {
			currentAimingOffset = Vector3.SmoothDamp(currentAimingOffset, targetAimingOffset, ref leadVelocity, aimingSmoothTime * Time.fixedDeltaTime);
		}
		
		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying || !target || isPaused) return;

			Gizmos.DrawRay(transform.position, transform.forward);

			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(CalculateNewPos(), 0.25f);

			Gizmos.color = Color.red;
			Gizmos.DrawSphere(CalculateTargetPos(), 0.25f);
		}
	}
}