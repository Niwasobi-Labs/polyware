using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace PolyWare.Utils {
	[RequireComponent(typeof(NavMeshAgent))]
	public class ClickToMove : MonoBehaviour {
		private NavMeshAgent agent;
		private Camera cam;

		private void Awake() {
			agent = GetComponent<NavMeshAgent>();
		}

		private void Start() {
			cam = Core.Instance.CameraManager.CameraRef;
		}

		private void Update() {
			if (!Mouse.current.leftButton.wasPressedThisFrame) return;

			if (Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit)) agent.SetDestination(hit.point);
		}
	}
}