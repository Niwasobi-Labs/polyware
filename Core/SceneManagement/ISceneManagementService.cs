using System;
using System.Threading.Tasks;

namespace PolyWare.Core {
	public interface ISceneManagementService : IService {
		event Action<string> OnSceneGroupLoaded;
		event Action<float> OnSceneGroupLoadProgress;

		static string CoreSceneName = "Core";
		
		Task LoadSceneGroup(string groupName);
		Task LoadRandomSceneGroup();
		Task ReloadActiveSceneGroup();
	}
}