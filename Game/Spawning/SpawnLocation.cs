using UnityEngine;

namespace PolyWare.Game {
	public struct SpawnLocation {
		public Vector3 Position;
		public Quaternion Rotation;

		public SpawnLocation(Vector3 position, Quaternion rotation) {
			this.Position = position;
			this.Rotation = rotation;
		}
		
		public SpawnLocation(Transform transform) {
			Position = transform.position;
			Rotation = transform.rotation;
		}
	}
}