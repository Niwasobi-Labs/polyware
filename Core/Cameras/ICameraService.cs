using UnityEngine;

namespace PolyWare.Core {
	public interface ICameraService : IService {
		public Camera ActiveCamera { get; }
		public FMODUnity.StudioListener FMODListener { get; }
		public void SetActiveCamera(Camera newActiveCamera);
		public void SetTarget(Transform target);
		public void SwapToPreviousTarget();
		public void SetPaused(bool isPaused);
	}
}