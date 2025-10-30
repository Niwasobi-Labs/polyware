using UnityEngine;

namespace PolyWare.Game {
	public class FollowTarget : MonoBehaviour {
		[SerializeField] private GameObject target;
		[SerializeField] private Vector3 offset;

		private void Update() {
			if (target) transform.position = target.transform.position + offset;
		}
	}
}