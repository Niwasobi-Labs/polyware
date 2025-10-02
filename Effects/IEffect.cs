using System;
using PolyWare.Core;

namespace PolyWare.Effects {
	// todo: add a IEffectFactory to avoid heap allocations
	public interface IEffect {
		event Action<IEffect> OnCompleted;
		
		public void Apply(IAffectable target, ContextHolder ctx);
		void Update(float deltaTime); // todo: can this be removed in favor of ImprovedTimers?
		public void Cancel();
	}
}