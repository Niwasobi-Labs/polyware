using UnityEngine;

namespace PolyWare.Interactions {
	public interface IProximityUser {
		public GameObject GetUserObject();
		public void NotifyTargetInRange(IProximityTarget target);
		public void NotifyTargetOutOfRange(IProximityTarget target);
	}
}