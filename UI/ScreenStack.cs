using System;
using System.Collections.Generic;
using PolyWare.Debug;

namespace PolyWare.UI {
	public class ScreenStack {
		private readonly Dictionary<Type, UIScreen> screenCache = new();
		private readonly Stack<UIScreen> screenStack = new();

		public UIScreen PushScreen(UIScreen uiScreen) {
			screenStack.Push(uiScreen);
			return uiScreen;
		}

		public void PopScreen() {
			if (screenStack.Count == 0) return;

			UIScreen topUIScreen = screenStack.Pop();
			topUIScreen.Close();

			if (topUIScreen.Persistant) CacheScreen(topUIScreen);
			else topUIScreen.Close();

			if (screenStack.Count <= 0) return;

			UIScreen newTopUIScreen = screenStack.Peek();
			newTopUIScreen.Open();
		}

		public UIScreen GetTopScreen() {
			return screenStack.Count > 0 ? screenStack.Peek() : null;
		}

		public UIScreen CheckForCachedScreen(Type screenUI) {
			if (screenCache.TryGetValue(screenUI, out UIScreen newScreen) && !newScreen.IsOpen) return newScreen;

			return null;
		}

		private void CacheScreen(UIScreen uIUIScreen) {
			if (!screenCache.TryAdd(uIUIScreen.GetType(), uIUIScreen) && uIUIScreen != screenCache[uIUIScreen.GetType()]) Log.Error("Persistant screen already exists, can't cache: " + uIUIScreen.gameObject.name);
		}
	}
}