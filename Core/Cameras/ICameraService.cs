using UnityEngine;

namespace PolyWare.Core {
	public interface ICameraService : IService {
		public Camera ActiveCamera { get; }
		public FMODUnity.StudioListener FMODListener { get; }
		public void SetActiveCamera(Camera newActiveCamera);
		public void SetTarget(Transform target);
		public void FollowPlayer();
		public void SetPaused(bool isPaused);
		public void Shake(CameraShakeSettings shakeSettings);
	}
}