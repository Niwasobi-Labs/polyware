using System;
using System.Threading.Tasks;
using PolyWare.Core.Services;

namespace PolyWare.Core.SceneManagement {
	public interface ISceneManagementService : IService {
		event Action<string> OnSceneGroupLoaded;
		event Action<float> OnSceneGroupLoadProgress;

		static string CoreSceneName = "Core";
		
		Task LoadSceneGroup(string groupName);
		Task ReloadActiveSceneGroup();
	}
}