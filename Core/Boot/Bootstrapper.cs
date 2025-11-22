using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolyWare.Core {
	public class Bootstrapper : MonoBehaviour {
		
		[SerializeField] private SceneGroupCollection sceneGroupCollection;
		[SerializeField] private string coreSceneGroupID;
		[SerializeField] private string initialSceneGroupID;

		private bool isEditor;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void EnsureCoreLoaded() {
			if (!SceneManager.GetSceneByName("Core").IsValid())  SceneManager.LoadScene("Core", LoadSceneMode.Additive);
		}
		
		private void Awake() {
			isEditor = Application.isEditor;
			SetupCoreServices();
		}

		private void SetupCoreServices() {
			ServiceLocator.Global.Register<ITelemetryService>(new LocalTelemetryService());
			ServiceLocator.Global.Register<ISceneManagementService>(new SceneManagementService(new SimpleSceneGroupManager(), sceneGroupCollection));
			ServiceLocator.Global.Register<IInputService>(new NullInputService());
		}

		private async void Start() {
			if (initialSceneGroupID != string.Empty)
				await ServiceLocator.Global.Get<ISceneManagementService>().LoadSceneGroup(coreSceneGroupID);

			if (!isEditor) {
				await ServiceLocator.Global.Get<ISceneManagementService>().LoadSceneGroup(initialSceneGroupID);
			}
		}
	}
}