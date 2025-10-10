using PolyWare.Core.Services;
using UnityEngine;

namespace PolyWare.Cameras {
	public class CameraService : MonoBehaviour, ICameraService {
		public Camera ActiveCamera { get; private set; }

		private void Awake() {
			ServiceLocator.Global.Register<ICameraService>(this);
		}

		public void SetActiveCamera(Camera newActiveCamera) {
			ActiveCamera?.gameObject.SetActive(false);
			ActiveCamera = newActiveCamera;
			ActiveCamera.gameObject.SetActive(true);
		}

		public void SetTarget(Transform target) {
			
		}
		
		public void SwapToPreviousTarget() {
			
		}
		
		public void SetPaused(bool isPaused) {
			
		}
	}
}