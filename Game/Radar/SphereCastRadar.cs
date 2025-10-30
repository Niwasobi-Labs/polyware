using UnityEngine;

namespace PolyWare.Game {
	public class SphereCastRadar : IRadarStrategy {
		private Collider[] results;
		
		public void Initialize(RadarSettings radarSettings) {
			results = new Collider[Mathf.Max(radarSettings.MaxLocalResults, radarSettings.MaxDistantResults)];
		}
		
		public void Scan(Transform origin, RadarData.ScanData data, float range, int layerMask) {
			int hits = Physics.OverlapSphereNonAlloc(origin.position, range, results, layerMask);
			IRadarStrategy.SimpleFilterColliders(hits, results, data);
		}
		
		public void DrawGizmos(Transform origin, float range) {
			Gizmos.DrawWireSphere(origin.position, range);
		}
	}
}