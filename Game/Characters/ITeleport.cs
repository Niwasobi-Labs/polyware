using UnityEngine;

namespace PolyWare.Characters {
	public interface ITeleport {
		public void Teleport(Transform target, bool setRotation = true);
	}
}