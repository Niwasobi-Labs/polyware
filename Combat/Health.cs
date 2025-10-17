using System.Collections.Generic;
using PolyWare.Characters;
using PolyWare.Stats;
using PolyWare.Timers;
using PolyWare.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Combat {
	public class Health {
		
		private HealthData health;
		
		public struct HealthContext : IContext {
			public float Current;
			public float Max;
		}
		
		public event UnityAction<HealthContext> OnDamageTaken;
		public event UnityAction<HealthContext> OnHeal;
		public event UnityAction OnRegenStart;
		public event UnityAction<HealthContext> OnRegenUpdate;
		public event UnityAction<HealthContext> OnRegenComplete;
		public event UnityAction<DamageContext> OnDeath;
			
		public CountdownTimer RegenTimer;
		public CountdownTimer RegenDelayTimer;

		protected readonly List<Timer> timers = new List<Timer>();

		public float MaxHealth => health.MaxHealth(myCharacter);
		public float CurrentHealth => health.Current;

		private ICharacter myCharacter;
		
		public void Initialize(HealthData newHealth, ICharacter character = null) {
			health = newHealth;
			myCharacter = character;
			
			SetupTimers();
			health.Current = MaxHealth;
		}
		
		public void Update() {
			foreach (Timer timer in timers) timer.Tick(Time.deltaTime);
		}
		
		private void SetupTimers() {
			if (RegenTimer == null) {
				timers.Add(RegenTimer = new CountdownTimer(health.RegenTime) {
					OnTimerTick = UpdateRegen,
					OnTimerComplete = CompleteRegen
				});
			}
			else {
				RegenTimer.SetInitialTime(health.RegenTime);
			}

			if (RegenDelayTimer == null) {
				timers.Add(RegenDelayTimer = new CountdownTimer(health.RegenDelayTime) {
					OnTimerComplete = StartRegen
				});
			}
			else {
				RegenDelayTimer.SetInitialTime(health.RegenDelayTime);
			}
		}

		public bool IsAlive() {
			return health.Current > 0;
		}
		
		public void Die(DamageContext damageContext) {
			OnDeath?.Invoke(damageContext);
		}
		
		public void TakeDamage(DamageContext ctx) {
			if (health.Invincible) return;
			if (health.Current <= 0) return;
			
			RegenTimer.Stop();
			RegenDelayTimer.Restart();
			
			health.Current = Mathf.Max(health.Current - ctx.Damage, 0);

			OnDamageTaken?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth });
			
			if (health.Current <= 0) Die(ctx);
		}
		
		public void Heal(float healAmount) {
			if (!health.CanHeal) return;
			
			health.Current = Mathf.Min(health.Current + healAmount, MaxHealth);
			OnHeal?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth });
		}

		public void StartRegen() {
			if (!health.CanRegen) return;

			RegenTimer.StartAt(1 - health.Current / MaxHealth);
			OnRegenStart?.Invoke();
		}

		private void UpdateRegen() {
			health.Current = Mathf.Lerp(0, MaxHealth, 1 - RegenTimer.Progress);
			OnRegenUpdate?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth });
		}

		private void CompleteRegen() {
			health.Current = MaxHealth;
			OnRegenComplete?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth });
		}
	}
}