using System;
using System.Threading.Tasks;

namespace PolyWare.Core.SceneManagement {
	public interface ISceneGroupManager {
		public Task LoadScenes(SceneGroup group, IProgress<float> progress, bool reloadDupScenes = false);
		public Task UnloadScenes();
	}
}