using System;
using PolyWare.Abilities;
using PolyWare.Combat;
using PolyWare.Timers;

namespace PolyWare.Effects {
	public class DamageOverTimeEffect : IEffect<IDamageable> {
		public float Interval = 1;
		public float Duration = 2;

		private IntervalTimer timer;
		private IDamageable currentTarget;
		private DamageEffectContext currentContext;
		
		public event Action<IEffect<IDamageable>> OnCompleted;
		
		public void Apply(IDamageable target, AbilityContext context) {
			currentTarget = target;
			currentContext = context.Get<DamageEffectContext>();
			
			timer = new IntervalTimer(Duration, Interval);
			timer.OnInterval += OnInterval;
			timer.OnTimerComplete += Cleanup;
			timer.Start();
		}

		private void OnInterval() {
			currentTarget?.TakeDamage(currentContext.DamageInfo);
		}
		
		public void Update(float deltaTime) {
			timer.Tick(deltaTime);
		}
		
		public void Cancel() {
			timer.Stop();
			Cleanup();
		}

		private void Cleanup() {
			currentTarget = null;
			timer = null;
			OnCompleted?.Invoke(this);
		}
	}
}