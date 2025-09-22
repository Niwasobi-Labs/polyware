using PolyWare.Debug;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Gameplay {
	public class ScreenSpaceRadar : IRadarStrategy {
		private struct BoxCastData {
			public Vector3 Center;
			public Vector3 HalfExtents;
			public Quaternion Rotation;
		}
		
		private BoxCastData currentBox;
		private Collider[] results;
		
		public void Initialize(RadarSettings radarSettings) {
			results = new Collider[Mathf.Max(radarSettings.MaxLocalResults, radarSettings.MaxDistantResults)];
		}
		
		public void Scan(Transform origin, RadarData.ScanData data, float range, int layerMask) {
			RebuildBox();
			int hits = Physics.OverlapBoxNonAlloc(currentBox.Center, currentBox.HalfExtents * range, results, currentBox.Rotation, layerMask);
			IRadarStrategy.SimpleFilterColliders(hits, results, data);
		}

		public void DrawGizmos(Transform origin, float range) {
			RebuildBox();
			GizmoHelper.DrawRotatedCube(currentBox.Center, currentBox.HalfExtents * 2f * range, currentBox.Rotation);
		}
		
		private void RebuildBox() {
			Camera cam = Core.Instance.CameraManager.CameraRef;
			
			Vector3 bottomLeft = cam.ViewportToWorldPoint(Constants.UnitBottomLeft);
			Vector3 topLeft = cam.ViewportToWorldPoint(Constants.UnitTopLeft);
			Vector3 topRight = cam.ViewportToWorldPoint(Constants.UnitTopRight);
			Vector3 bottomRight = cam.ViewportToWorldPoint(Constants.UnitBottomRight);

			float width = Vector3.Distance(bottomLeft, bottomRight);
			float height = Vector3.Distance(bottomLeft, topLeft);
			
			currentBox.Center = (bottomLeft + bottomRight + topLeft + topRight) * 0.25f;
			currentBox.HalfExtents.x = width * 0.5f;
			currentBox.HalfExtents.y = height * 0.5f;
			currentBox.HalfExtents.z = cam.farClipPlane;
			currentBox.Rotation = cam.transform.rotation;
		}
	}
}