using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	public class LaserSight : MonoBehaviour {
		[SerializeField][Required] private LineRenderer lineRenderer;
		
		private float laserRange = 100f;
		private int blockingMask;

		private void Awake() {
			blockingMask = 1 << LayerMask.NameToLayer("Default");
		}

		public void SetRange(float range) {
			laserRange = range;
		}

		public void SetStatus(bool status) {
			lineRenderer.enabled = status;
		}

		private void Update() {
			if (!lineRenderer.enabled) return;

			Vector3 endPos = transform.position + transform.forward * laserRange;
			
			if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, laserRange, blockingMask)) {
				endPos = hit.point;
			}

			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, endPos);
		}
	}
}