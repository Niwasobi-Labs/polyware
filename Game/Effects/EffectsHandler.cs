using System;
using System.Collections.Generic;

namespace PolyWare.Game {
	public class EffectsHandler : IEffectsHandler {
		public event Action OnEmpty;
		
		private readonly List<IEffect> activeEffects = new();
		public IReadOnlyList<IEffect> ActiveEffects => activeEffects;
		
		public void Add(IEffect effect) {
			effect.OnCompleted += Remove;
			activeEffects.Add(effect);
		}

		public void Update(float deltaTime) {
			for (int i = 0; i < activeEffects.Count; ++i) {
				activeEffects[i].Update(deltaTime);
			}
		}

		public void Remove(IEffect effect) {
			activeEffects.Remove(effect);
			if (activeEffects.Count == 0) OnEmpty?.Invoke();
		}

		public void RemoveAll() {
			for (int i = 0; i < activeEffects.Count; i++) {
				activeEffects[i].Cancel();
			}
			activeEffects.Clear();
			OnEmpty?.Invoke();
		}

		public bool IsEmpty => activeEffects.Count == 0;
	}
}