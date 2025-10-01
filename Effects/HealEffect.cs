using System;
using PolyWare.Abilities;
using PolyWare.Combat;
using PolyWare.Core;

namespace PolyWare.Effects {
	public class HealEffectContext : IContext {
		public float Health;
		
		public HealEffectContext(float health) {
			Health = health;
		}
	}
	
	public class HealEffect : IEffect<IDamageable> {
		public event Action<IEffect<IDamageable>> OnCompleted;
		
		public void Apply(IDamageable target, AbilityContext context) {
			target.Heal(context.Get<HealEffectContext>().Health);
			OnCompleted?.Invoke(this);
		}
		
		public void Update(float deltaTime) {
			// no op
		}
		
		public void Cancel() {
			// no op
		}
	}
}