using System.Collections.Generic;
using PolyWare.Debug;

namespace PolyWare.Gameplay {
	public class RadarData {
		public class ScanData {
			public int Count => Results.Count;
			
			public readonly List<IRadarTarget> Results;
			
			public ScanData(int capacity) {
				Results = new List<IRadarTarget>(capacity);
			}

			public void Clear() {
				Results.Clear();
			}
		}

		public readonly ScanData Local;
		public readonly ScanData Distant;

		public RadarData(RadarSettings settings) {
			Local = new ScanData(settings.MaxLocalResults);
			Distant = new ScanData(settings.MaxDistantResults);
		}

		public void Clear() {
			Local.Clear();
			Distant.Clear();
		}

		public void Print() => Log.Message($"Local Count: {Local.Count},  Distant Count: {Distant.Count}");
	}
}