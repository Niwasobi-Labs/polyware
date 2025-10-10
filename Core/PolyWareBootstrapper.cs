using PolyWare.Analytics.Telemetry;
using PolyWare.AssetManagement;
using PolyWare.Core.SceneManagement;
using PolyWare.Core.Services;
using PolyWare.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolyWare.Core {
	public class PolyWareBootstrapper : MonoBehaviour {
		
		[SerializeField] private Collector collector;
		[SerializeField] private SceneGroupCollection sceneGroupCollection;
		[SerializeField] private string initialSceneGroupID;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void EnsureCoreLoaded() {
			if (!SceneManager.GetSceneByName("Core").IsValid())  SceneManager.LoadScene("Core", LoadSceneMode.Additive);
		}
		
		private void Awake() {
			SetupCoreServices();
		}

		private void SetupCoreServices() {
			ServiceLocator.Global.Register<IAssetManagementService>(new AssetManagementService(collector));
			ServiceLocator.Global.Register<ITelemetryService>(new LocalTelemetryService());
			ServiceLocator.Global.Register<ISceneManagementService>(new SceneManagementService(new SimpleSceneGroupManager(), sceneGroupCollection));
			ServiceLocator.Global.Register<IInputService>(new NullInputService());
		}

		private void Start() {
			if (initialSceneGroupID != string.Empty)
				ServiceLocator.Global.Get<ISceneManagementService>().LoadSceneGroup(initialSceneGroupID);
		}
	}
}