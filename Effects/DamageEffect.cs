using System;
using PolyWare.Abilities;
using PolyWare.Combat;
using PolyWare.Core;

namespace PolyWare.Effects {
	public class DamageEffectContext : IContext {
		public DamageInfo DamageInfo;

		public DamageEffectContext(DamageInfo damageInfo) {
			DamageInfo = damageInfo;
		}
	}
	
	public class DamageEffect : IEffect<IDamageable> {
		
		public event Action<IEffect<IDamageable>> OnCompleted;
		
		public void Apply(IDamageable target, AbilityContext context) {
			target.TakeDamage(context.Get<DamageEffectContext>().DamageInfo);
			OnCompleted?.Invoke(this);
		}

		public void Update(float deltaTime) {
			// noop
		}

		public void Cancel() {
			// noop
		}
	}
}