using UnityEngine;

namespace PolyWare.UI.Utils {
	public class Billboard : MonoBehaviour {
		private void FixedUpdate() {
			transform.LookAt(Core.Instance.CameraManager.CameraRef.transform, Vector3.up);
			transform.localRotation *= Quaternion.Euler(0, 180, 0);
		}
	}
}