using UnityEngine;

namespace PolyWare.Interaction {
	public interface IProximityUser {
		public GameObject GetUserObject();
		public void OnTargetEnteredRange(IProximityTarget target);
		public void OnTargetExitedRange(IProximityTarget target);
	}
}