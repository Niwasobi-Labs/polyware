using UnityEngine;

namespace PolyWare.Core {
	public class Billboard : MonoBehaviour {
		private void FixedUpdate() {
			transform.LookAt(ServiceLocator.Global.Get<ICameraService>().ActiveCamera.transform, Vector3.up);
			transform.localRotation *= Quaternion.Euler(0, 180, 0);
		}
	}
}