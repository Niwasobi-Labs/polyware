using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Interactions {
	public class ProximityDetector : MonoBehaviour, IProximityTarget {
		[Header("ProximityDetector")] [SerializeField]
		protected UnityEvent onTriggerEnterEvents;

		[SerializeField] protected UnityEvent onTriggerExitEvents;

		protected Collider MyCollider;

		private void Awake() {
			if (!GetComponent<Collider>()) Debug.Logger.Error($"{name} is missing a collider for it's ProximityDetector");
		}

		private void OnTriggerEnter(Collider other) {
			if (!other.TryGetComponent(out IProximityUser user)) return;
			
			user.NotifyTargetInRange(this);
			OnProximityUserEnter(user);
		}

		private void OnTriggerExit(Collider other) {
			if (!other.TryGetComponent(out IProximityUser user)) return;
			
			user.NotifyTargetOutOfRange(this);
			OnProximityUserExit(user);
		}

		// IProximityTarget
		public GameObject GetGameObject() {
			return gameObject;
		}

		protected virtual void OnProximityUserEnter(IProximityUser user) {
			onTriggerEnterEvents.Invoke();
		}

		protected virtual void OnProximityUserExit(IProximityUser user) {
			onTriggerExitEvents.Invoke();
		}
	}
}