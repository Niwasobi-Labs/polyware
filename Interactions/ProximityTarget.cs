using PolyWare.Debug;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Interactions {
	public class ProximityTarget : MonoBehaviour, IProximityTarget {
		[Header("ProximityDetector")] 
		[SerializeField] protected UnityEvent onTriggerEnterEvents;
		[SerializeField] protected UnityEvent onTriggerExitEvents;
		[SerializeField] protected Collider MyCollider;

		private void OnTriggerEnter(Collider other) {
			if (!other.TryGetComponent(out IProximityUser user)) return;
			
			user.OnTargetEnteredRange(this);
			OnProximityUserEnter(user);
		}

		private void OnTriggerExit(Collider other) {
			if (!other.TryGetComponent(out IProximityUser user)) return;
			
			user.OnTargetExitedRange(this);
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