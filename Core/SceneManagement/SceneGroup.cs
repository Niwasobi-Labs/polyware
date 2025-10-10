using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace PolyWare.Core.SceneManagement {
	[Serializable]
	public class SceneGroup {
		public List<SceneData> Scenes;

		public string FindActiveScene() {
			return Scenes.FirstOrDefault(scene => scene.SetToActiveOnLoad)?.Name;
		}
		
		public string FindSceneNameByType(SceneType sceneType) {
			return Scenes.FirstOrDefault(scene => scene.Type == sceneType)?.Name;
		}
		
		public SceneType FindSceneTypeByName(string sceneName) {
			foreach (SceneData sceneData in Scenes.Where(sceneData => sceneData.Name == sceneName)) {
				return sceneData.Type;
			}

			return SceneType.None;
		}
	}
	
	[Serializable]
	public class SceneData {
		public SceneReference Reference;
		public string Name => Reference.Name;
		public SceneType Type;
		public bool SetToActiveOnLoad = false;
	}
	
	public enum SceneType { Core, Level, Environment, None }
}