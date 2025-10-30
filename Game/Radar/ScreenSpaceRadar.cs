using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class ScreenSpaceRadar : IRadarStrategy {
		
		private CastingBox currentCastingBox;
		private Collider[] results;
		
		public void Initialize(RadarSettings radarSettings) {
			results = new Collider[Mathf.Max(radarSettings.MaxLocalResults, radarSettings.MaxDistantResults)];
		}
		
		public void Scan(Transform origin, RadarData.ScanData data, float range, int layerMask) {
			currentCastingBox.Rebuild();
			int hits = Physics.OverlapBoxNonAlloc(currentCastingBox.Center, currentCastingBox.HalfExtents * range, results, currentCastingBox.Rotation, layerMask);
			IRadarStrategy.SimpleFilterColliders(hits, results, data);
		}

		public void DrawGizmos(Transform origin, float range) {
			currentCastingBox.Rebuild();
			GizmoHelper.DrawRotatedCube(currentCastingBox.Center, currentCastingBox.HalfExtents * 2f * range, currentCastingBox.Rotation);
		}
	}
}