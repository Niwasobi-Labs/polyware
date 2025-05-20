using System;
using System.Collections.Generic;

namespace PolyWare.UI {
	public class ScreenStack {
		private readonly Dictionary<Type, Screen> screenCache = new();
		private readonly Stack<Screen> screenStack = new();

		public Screen PushScreen(Screen screen) {
			screenStack.Push(screen);
			return screen;
		}

		public void PopScreen() {
			if (screenStack.Count == 0) return;

			Screen topScreen = screenStack.Pop();
			topScreen.Close();

			if (topScreen.Persistant) CacheScreen(topScreen);
			else topScreen.Close();

			if (screenStack.Count <= 0) return;

			Screen newTopScreen = screenStack.Peek();
			newTopScreen.Open();
		}

		public Screen GetTopScreen() {
			return screenStack.Count > 0 ? screenStack.Peek() : null;
		}

		public Screen CheckForCachedScreen(Type screenUI) {
			if (screenCache.TryGetValue(screenUI, out Screen newScreen) && !newScreen.IsOpen) return newScreen;

			return null;
		}

		private void CacheScreen(Screen uIScreen) {
			if (!screenCache.TryAdd(uIScreen.GetType(), uIScreen) && uIScreen != screenCache[uIScreen.GetType()]) Debug.Logger.Error("Persistant screen already exists, can't cache: " + uIScreen.gameObject.name);
		}
	}
}