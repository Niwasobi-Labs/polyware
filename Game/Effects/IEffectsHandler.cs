using System;
using System.Collections.Generic;

namespace PolyWare.Game {
	public interface IEffectsHandler {
		public event Action OnEmpty;
		public void Add(IEffect effect);
		public void Update(float deltaTime);
		public float GetStatusEffectStackOfType(StatusEffectType statusEffectType);
		public void Remove(IEffect effect);
		public void RemoveAll();
		public bool IsEmpty { get; }
	}
}