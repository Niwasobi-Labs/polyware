using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Game {
	public class ProximityTarget : MonoBehaviour, IProximityTarget {
		[Header("Proxmity Target")]
		public GameObject TargetOverride;
		[SerializeField] protected UnityEvent<IProximityUser> onTriggerEnterEvents;
		[SerializeField] protected UnityEvent<IProximityUser> onTriggerExitEvents;
		[SerializeField] protected Collider MyCollider;
		
		// IProximityTarget
		public GameObject GetTargetGameObject() {
			return TargetOverride ? TargetOverride : gameObject;
		}
		
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
		
		protected virtual void OnProximityUserEnter(IProximityUser user) {
			onTriggerEnterEvents.Invoke(user);
		}

		protected virtual void OnProximityUserExit(IProximityUser user) {
			onTriggerExitEvents.Invoke(user);
		}
	}
}