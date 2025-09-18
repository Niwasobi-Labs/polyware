using UnityEngine;

namespace PolyWare.Cameras {
	public abstract class CameraManager : MonoBehaviour, ICameraManager {
		[SerializeField] private Camera cam;
		public Camera CameraRef => cam;
	}
}