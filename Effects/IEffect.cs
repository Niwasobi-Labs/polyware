using System;
using PolyWare.Abilities;
using PolyWare.Core;

namespace PolyWare.Effects {

	public class EffectContext {
		//public float CriticalChance;
	}
	
	// todo: add a IEffectFactory to avoid heap allocations
	public interface IEffect<T> {
		event Action<IEffect<T>> OnCompleted;
		public void Apply(T target, AbilityContext effectContext);
		void Update(float deltaTime); // todo: can this be removed in favor of ImprovedTimers?
		public void Cancel();
	}
}