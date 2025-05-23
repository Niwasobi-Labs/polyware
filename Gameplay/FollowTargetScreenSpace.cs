using UnityEngine;

namespace PolyWare.Gameplay {
	[RequireComponent(typeof(RectTransform))]
	public class FollowTargetScreenSpace : MonoBehaviour {
		[SerializeField] private Vector3 offset;
		private UnityEngine.Camera cam;
		private RectTransform rect;
		private GameObject target;

		private void Awake() {
			rect = GetComponent<RectTransform>();
		}

		private void Update() {
			if (!target) return;

			rect.position = cam.WorldToScreenPoint(target.transform.position) + offset;
		}

		public void SetTarget(GameObject newTarget, UnityEngine.Camera newCamera) {
			target = newTarget;
			cam = newCamera;
		}
	}
}