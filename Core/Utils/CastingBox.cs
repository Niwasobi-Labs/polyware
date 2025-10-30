using UnityEngine;

namespace PolyWare.Core {
	public struct CastingBox {
		public Vector3 Center;
		public Vector3 HalfExtents;
		public Quaternion Rotation;
			
		public void Rebuild() {
			Camera cam = ServiceLocator.Global.Get<ICameraService>().ActiveCamera;
			
			Vector3 bottomLeft = cam.ViewportToWorldPoint(Constants.UnitBottomLeft);
			Vector3 topLeft = cam.ViewportToWorldPoint(Constants.UnitTopLeft);
			Vector3 topRight = cam.ViewportToWorldPoint(Constants.UnitTopRight);
			Vector3 bottomRight = cam.ViewportToWorldPoint(Constants.UnitBottomRight);

			float width = Vector3.Distance(bottomLeft, bottomRight);
			float height = Vector3.Distance(bottomLeft, topLeft);
			
			Center = (bottomLeft + bottomRight + topLeft + topRight) * 0.25f;
			HalfExtents.x = width * 0.5f;
			HalfExtents.y = height * 0.5f;
			HalfExtents.z = cam.farClipPlane;
			Rotation = cam.transform.rotation;
		}
	}
}