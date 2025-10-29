using PolyWare.Core;

namespace PolyWare.AssetManagement {
	public class AssetManagementService : IAssetManagementService {
		public Collector Collector { get; set; }

		public AssetManagementService(Collector collector) {
			Collector = collector;
			collector.Initialize();
		}
	}
}