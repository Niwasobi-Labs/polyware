using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Core {
	public class ScreenStack : MonoBehaviour {
		[field: SerializeField] public bool GrabInputFocus { get; private set; } = false;
		[field: SerializeField] public bool PauseGame { get; private set; } = false;
		
		public bool IsOpen { get; private set; } = false;
		
		private readonly Stack<UIScreen> screenStack = new();
		private float previousTimeScale;
		
		public UIScreen PushScreen(UIScreen uiScreen) {
			screenStack.Push(uiScreen);
			
			if (!IsOpen) Open();
			
			uiScreen.Open();
			
			return uiScreen;
		}

		public void Open() {
			gameObject.SetActive(true);
			IsOpen = true;

			if (!PauseGame) return;
			
			previousTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}

		public void Focus() {
			if (!GrabInputFocus) return;
			
			ServiceLocator.Global.Get<IInputService>().TogglePlayerInput(false);
			ServiceLocator.Global.Get<IInputService>().ToggleUIInput(true);
			GetTopScreen().Focus();
		}
		
		public void Close() {
			gameObject.SetActive(false);
			IsOpen = false;
			if (GrabInputFocus) {
				ServiceLocator.Global.Get<IInputService>().ToggleUIInput(false);
				ServiceLocator.Global.Get<IInputService>().TogglePlayerInput(true);
			}
			
			if (PauseGame) {
				Time.timeScale = previousTimeScale;
			}
		}

		public void Clear() {
			while (screenStack.Count > 0) {
				PopScreen();
			}
		}
		
		public void PopScreen() {
			if (screenStack.Count == 0) return;

			UIScreen topUIScreen = screenStack.Pop();
			topUIScreen.Close();
			
			// todo: Re-enable ui screen caching (https://niwasobi-labs.codecks.io/card/1cy-re-enable-screen-caching)
			Destroy(topUIScreen.gameObject);
			
			if (screenStack.Count <= 0) Close();
		}

		public UIScreen GetTopScreen() {
			return screenStack.Count > 0 ? screenStack.Peek() : null;
		}
		
		// private readonly Dictionary<Type, UIScreen> screenCache = new(); // todo: Re-enable ui screen caching (https://niwasobi-labs.codecks.io/card/1cy-re-enable-screen-caching)
		// public UIScreen CheckForCachedScreen(Type screenUI) {
		// 	if (screenCache.TryGetValue(screenUI, out UIScreen newScreen) && !newScreen.IsOpen) return newScreen;
		//
		// 	return null;
		// }
		//
		// private void CacheScreen(UIScreen uiScreen) {
		// 	if (!screenCache.TryAdd(uiScreen.GetType(), uiScreen) && uiScreen != screenCache[uiScreen.GetType()]) Log.Error("Persistant screen already exists, can't cache: " + uiScreen.gameObject.name);
		// }
	}
}