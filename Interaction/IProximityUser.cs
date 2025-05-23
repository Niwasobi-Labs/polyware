using UnityEngine;

namespace PolyWare.Interaction {
	public interface IProximityUser {
		public GameObject GetUserObject();
		public void NotifyTargetInRange(IProximityTarget target);
		public void NotifyTargetOutOfRange(IProximityTarget target);
	}
}