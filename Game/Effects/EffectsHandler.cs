using System;
using System.Collections.Generic;

namespace PolyWare.Game {
	public class EffectsHandler : IEffectsHandler {
		public event Action OnEmpty;
		
		private readonly List<IEffect> activeEffects = new();
		
		public void Add(IEffect effect) {
			effect.OnCompleted += Remove;
			activeEffects.Add(effect);
		}

		public void Update(float deltaTime) {
			for (int i = 0; i < activeEffects.Count; ++i) {
				activeEffects[i].Update(deltaTime);
			}
		}

		public float GetStatusEffectStackOfType(StatusEffectType statusEffectType) {
			float stack = 0;
			for (int i = 0; i < activeEffects.Count; ++i) {
				if (activeEffects[i] is StatusEffect statusEffect && statusEffect.StatusEffectType == statusEffectType) {
					stack += statusEffect.Stack;
				}
			}
			return stack;
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