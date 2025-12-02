using UnityEngine;

namespace PolyWare.Core {
	public interface ICameraService : IService {
		public Camera ActiveCamera { get; }
		public void SetFollowTarget(Transform target);
		public void SetBoundingVolume(Collider volume);
		public void SetZoom(float zoom, bool instant = false);
		public void ResetZoom(bool instant = false);
		public void FollowPlayer();
		public void Shake(CameraShakeSettings shakeSettings);
	}
}