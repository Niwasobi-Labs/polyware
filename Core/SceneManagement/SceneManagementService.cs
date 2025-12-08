using System;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace PolyWare.Core {
	public class SceneManagementService : ISceneManagementService {
		
		public event Action<float> OnSceneGroupLoadProgress;
		public event Action<string> OnSceneGroupLoaded;
		
		private float targetProgress;
		private bool isLoading;

		private readonly ISceneGroupManager manager;
		private readonly SceneGroupCollection sceneGroups;
		
		private string previouslyLoadedSceneGroup; 		
		
		public SceneManagementService(ISceneGroupManager sceneGroupManager, SceneGroupCollection sceneGroupCollection) {
			manager = sceneGroupManager;
			sceneGroups = sceneGroupCollection;
			sceneGroups.Initialize();
		}
		
		public async Task LoadSceneGroup(string sceneGroupName) {
			OnSceneGroupLoadProgress?.Invoke(0);
			
			var progress = new LoadingProgress();
			progress.OnProgress += (prog) => OnSceneGroupLoadProgress?.Invoke(prog);
			
			//EnableLoadingCanvas();
			SceneGroup sceneGroup = sceneGroups.Get(sceneGroupName);
			await manager.LoadScenes(sceneGroup, progress);
			
			//EnableLoadingCanvas(false);
			
			previouslyLoadedSceneGroup =  sceneGroupName;
			
			OnSceneGroupLoadProgress?.Invoke(1);
			OnSceneGroupLoaded?.Invoke(sceneGroupName);
		}

		public async Task LoadRandomSceneGroup() {
			await LoadSceneGroup(sceneGroups.RandomItem(1));
		}

		public async Task ReloadActiveSceneGroup() {
			await LoadSceneGroup(previouslyLoadedSceneGroup);
		}

		// private void EnableLoadingCanvas(bool enable = true) {
		// 	isLoading = enable;
		// 	loadingCanvas.gameObject.SetActive(enable);
		// 	loadingCamera.gameObject.SetActive(enable);
		// }
	}
}

