using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PolyWare.Core.Services;
using PolyWare.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolyWare.Core.SceneManagement {
	public class SimpleSceneGroupManager : ISceneGroupManager {
		public event Action<string> OnSceneLoad;
		public event Action<string> OnSceneUnload;
		public event Action OnSceneGroupLoaded;
		
		private SceneGroup activeSceneGroup;
		
		public async Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false) {
			activeSceneGroup = group;
			var loadedScenes = new List<string>();

			await UnloadScenes();

			int sceneCount = SceneManager.sceneCount;
			
			for (int i = 0; i < sceneCount; ++i) {
				loadedScenes.Add(SceneManager.GetSceneAt(i).name);
			}
			
			int totalScenesToLoad = activeSceneGroup.Scenes.Count;
			
			var operationGroup = new AsyncOperationGroup(totalScenesToLoad);
			
			for (int i = 0; i < totalScenesToLoad; ++i) {
				SceneData sceneData = group.Scenes[i];
				if (!reloadDupScenes && loadedScenes.Contains(sceneData.Name)) continue;
				
				AsyncOperation operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
				operationGroup.Operations.Add(operation);
				
				OnSceneLoad?.Invoke(sceneData.Name);
			}

			while (!operationGroup.IsDone) {
				progress?.Report(operationGroup.Progress);
				await Task.Delay(100); // avoid unnecessary reports with a manual delay (adjust if necessary)
			}
			
			Scene activeScene = SceneManager.GetSceneByName(activeSceneGroup.FindActiveScene());

			if (activeScene.IsValid()) {
				SceneManager.SetActiveScene(activeScene);
			}
			
			OnSceneGroupLoaded?.Invoke();
		}

		public async Task UnloadScenes() {
			var scenesToUnload = new List<string>();
			string activeScene = SceneManager.GetActiveScene().name;
			
			int sceneCount = SceneManager.sceneCount;

			for (int i = sceneCount - 1; i > 0; --i) {
				Scene sceneAt = SceneManager.GetSceneAt(i);

				ServiceLocator.RemoveSceneLocator(sceneAt);
				
				if (!sceneAt.isLoaded) continue;
				
				string sceneName = sceneAt.name;
				if (sceneName.Equals(activeScene) || sceneName == ISceneManagementService.CoreSceneName || activeSceneGroup.FindSceneTypeByName(sceneName) == SceneType.Core) continue;
				scenesToUnload.Add(sceneName);
			}
			
			
			var operationGroup = new AsyncOperationGroup(scenesToUnload.Count);
			
			for (int i = 0; i < scenesToUnload.Count; ++i) {
				AsyncOperation operation = SceneManager.UnloadSceneAsync(scenesToUnload[i]);
				if (operation == null) continue;
					
				operationGroup.Operations.Add(operation);
					
				OnSceneUnload?.Invoke(scenesToUnload[i]);
			}

			while (!operationGroup.IsDone) {
				await Task.Delay(100); // delay to avoid too quick of a loop
			}
		}
	}
}