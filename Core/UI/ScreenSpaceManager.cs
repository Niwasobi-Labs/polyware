using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Core {
	public class ScreenSpaceManager : MonoBehaviour {
		[SerializeField] protected ScreenRegistry screenRegistry;
		
		private readonly Dictionary<int, ScreenStack> screenStack = new();
		private Canvas canvas;
		private readonly Stack<int> layerFocusHistory = new();
		
		protected virtual void Awake() {
			screenRegistry.Initialize();
		}

		public void AddUIStackLayer(ScreenStack stack, int layer) {
			screenStack.Add(layer, stack);
		}
		
		public T PushScreen<T>(int layer, bool addToHistory = true) where T : UIScreen {
			var screen = screenRegistry.GetPrefab(typeof(T), screenStack[layer].transform) as T;

			if (!screen) {
				Log.Error($"Couldn't find screen of type: {typeof(T)}");
				return null;
			}
			
			screenStack[layer].PushScreen(screen);
			screenStack[layer].Focus();
			
			if (addToHistory) layerFocusHistory.Push(layer);
			
			
			return screen;
		}

		public void ClearLayer(int layer) {
			screenStack[layer].Clear();
		}

		public void ClearHistory() {
			layerFocusHistory.Clear();	
		}

		public UIScreen GetTopScreen() {
			return layerFocusHistory.Count > 0 ? screenStack[layerFocusHistory.Peek()].GetTopScreen() : null;
		}
		
		public void PopScreen() {
			int lastUsedLayer = layerFocusHistory.Pop();
			screenStack[lastUsedLayer].PopScreen();

			if (layerFocusHistory.Count > 0) {
				screenStack[layerFocusHistory.Peek()].Focus();	
			}
		}
	}
}