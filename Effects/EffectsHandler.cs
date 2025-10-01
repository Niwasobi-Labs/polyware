using System.Collections.Generic;
using PolyWare.Combat;

namespace PolyWare.Effects {
	public class EffectsHandler : IEffectsHandler {
		private readonly List<IEffect<IDamageable>> activeEffects = new();

		public void Add(IEffect<IDamageable> effect) {
			effect.OnCompleted += Remove;
			activeEffects.Add(effect);
		}

		public void Update(float deltaTime) {
			for (int i = 0; i < activeEffects.Count; ++i) {
				activeEffects[i].Update(deltaTime);
			}
		}
		
		public void Remove(IEffect<IDamageable> effect) {
			activeEffects.Remove(effect);
		}

		public void RemoveAll() {
			for (int i = 0; i < activeEffects.Count; i++) {
				activeEffects[i].Cancel();
			}
			activeEffects.Clear();
		}
	}
}