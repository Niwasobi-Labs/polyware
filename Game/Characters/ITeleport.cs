using UnityEngine;

namespace PolyWare.Game {
	public interface ITeleport {
		public void Teleport(Transform target, bool setRotation = true);
	}
}