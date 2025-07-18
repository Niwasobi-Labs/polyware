using UnityEngine;

namespace PolyWare.Interactions {
	public interface IProximityUser {
		public GameObject GetUserObject();
		public void OnTargetEnteredRange(IProximityTarget target);
		public void OnTargetExitedRange(IProximityTarget target);
	}
}