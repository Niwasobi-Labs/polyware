using UnityEngine;

namespace PolyWare.Core {
	public class ScreenSpaceManager : MonoBehaviour {
		[SerializeField] protected ScreenRegistry screenRegistry;
		
		private readonly ScreenStack screenStack = new();

		private Canvas canvas;

		public UIScreen TopUIScreen => screenStack.GetTopScreen();
		
		protected virtual void Awake() {
			screenRegistry.Initialize();
		}

		public T PushScreen<T>(bool overlay, bool autoOpen = true) where T : UIScreen {
			T screen = screenStack.CheckForCachedScreen(typeof(T)) as T ?? screenRegistry.GetPrefab(typeof(T), transform) as T;

			if (!screen) {
				Log.Error($"Couldn't find screen of type: {typeof(T)}");
				return null;
			}
			
			screenStack.PushScreen(screen);

			if (autoOpen) screen.Open();
			else screen.Close(); 
			
			return screen;
		}
	}
}