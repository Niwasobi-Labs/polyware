using PolyWare.Core;
using System;
using UnityEngine;

namespace PolyWare.Game {
	public interface IRadarStrategy {
		public enum RadarStrategyType {
			SphereCast,
			ScreenSpace
		}
		
		public void Initialize(RadarSettings radarSettings);
		
		public void Scan(Transform origin, RadarData.ScanData data, float range, int layerMask);
		
		public void DrawGizmos(Transform origin, float range);
		
		public static IRadarStrategy Create(RadarStrategyType type, RadarSettings settings) {
			IRadarStrategy newStrategy;
			try {
				newStrategy = type switch {
					RadarStrategyType.SphereCast => new SphereCastRadar(),
					RadarStrategyType.ScreenSpace => new ScreenSpaceRadar(),
					_ => throw new NotImplementedException()
				};
			}
			catch (Exception) {
				Log.Error($"IRadarStrategy is missing types {type}");
				throw;
			}
			
			newStrategy.Initialize(settings);
			return newStrategy;
		}
		
		protected static void SimpleFilterColliders(int hits, Collider[] rawData, RadarData.ScanData scanData) {
			for (int i = 0; i < hits; ++i) {
				if (!rawData[i] || !rawData[i].gameObject.TryGetComponent(out IRadarTarget radarObject)) continue;
				scanData.Results.Add(radarObject);
			}
		}
	}
}