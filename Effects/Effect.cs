using System;
using PolyWare.Core;

namespace PolyWare.Effects {
	public abstract class Effect<TTarget, TContext> : IEffect where TTarget : IAffectable where TContext : ContextHolder {
		public abstract event Action<IEffect> OnCompleted;
		
		public void Apply(IAffectable target, ContextHolder ctx) {
			OnApply((TTarget)target, (TContext)ctx);
		}
		
		protected abstract void OnApply(TTarget target, TContext ctx);
		
		public virtual void Update(float deltaTime) { }
		public virtual void Cancel() { }
	}
}