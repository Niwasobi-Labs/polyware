using PolyWare.AssetManagement;
using PolyWare.Core.Services;

namespace PolyWare.Core {
	public interface IAssetManagementService : IService {
		public Collector Collector { get; set; }
	}
}