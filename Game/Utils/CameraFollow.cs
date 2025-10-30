using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	public class CameraFollow : MonoBehaviour {
		[Title("Targeting")]
		[ShowInInspector] public Transform Target { get; private set; }
		
		[Title("Smoothing")]
		[SerializeField] private float smoothTime = 20f;
		[SerializeField] private float transitionSpeedDampener = 3f;

		[Title("Aiming Settings")]
		[SerializeField]  private float aimingLead = 5f;
		[SerializeField] private float aimingSmoothTime = 20f;
		
		private Vector3 currentAimingOffset = Vector3.zero;
		private Vector3 targetAimingOffset = Vector3.zero;
		
		private bool isAiming;
		private bool isPaused;
		
		private Vector3 leadVelocity = Vector3.zero;
		private Vector3 moveVelocity = Vector3.zero;
		
		private bool transitioningToNewTarget = false;
		private Transform previousTarget;
		private Transform defaultTarget;

		private void FixedUpdate() {
			if (isPaused || !Target) return;

			UpdateAimingOffset();

			if (transitioningToNewTarget) {
				transform.position = Vector3.SmoothDamp(transform.position, CalculateNewPos(), ref moveVelocity, (smoothTime * transitionSpeedDampener) * Time.fixedDeltaTime);
				
				if (Vector3.Distance(transform.position, Target.position) < 0.1f) transitioningToNewTarget = false;
			}
			else {
				transform.position = Vector3.SmoothDamp(transform.position, CalculateNewPos(), ref moveVelocity, smoothTime * Time.fixedDeltaTime);	
			}
		}
		
		public void SwapToLastTarget() {
			if (previousTarget) SetTarget(previousTarget);
		}
		
		public void SetTarget(Transform newTarget) {
			if (Target) previousTarget = Target;
			Target = newTarget;
			transitioningToNewTarget = true;
		}

		public void Pause() {
			isPaused = true;
		}

		public void Unpause() {
			isPaused = false;
			transform.position = CalculateTargetPos();
		}

		// todo: subscribe to the aim event instead of a public call
		private void ToggleAimingMode(bool status, Vector3 aimingDirection) {
			isAiming = status;

			if (isAiming)  targetAimingOffset = aimingDirection.normalized * aimingLead;
			else  targetAimingOffset = Vector3.zero;
		}
		
		private Vector3 CalculateTargetPos() {
			return Target ? Target.position + targetAimingOffset : Vector3.zero;
		}

		private Vector3 CalculateNewPos() {
			return Target.position + currentAimingOffset;
		}

		private void UpdateAimingOffset() {
			currentAimingOffset = Vector3.SmoothDamp(currentAimingOffset, targetAimingOffset, ref leadVelocity, aimingSmoothTime * Time.fixedDeltaTime);
		}
		
		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying || !Target || isPaused) return;

			Gizmos.DrawRay(transform.position, transform.forward);

			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(CalculateNewPos(), 0.25f);

			Gizmos.color = Color.red;
			Gizmos.DrawSphere(CalculateTargetPos(), 0.25f);
		}
	}
}