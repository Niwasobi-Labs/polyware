using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Game {
	public class Health {
		
		private HealthData health;
		
		public struct HealthContext : IContext {
			public float Current;
			public float Max;
			public float Overcharge;
			public bool FinalStand;
		}
		
		public event UnityAction<HealthContext> OnDamageTaken;
		public event UnityAction<HealthContext> OnHeal;
		public event UnityAction OnRegenStart;
		public event UnityAction<HealthContext> OnRegenUpdate;
		public event UnityAction<HealthContext> OnRegenComplete;
		public event UnityAction<DamageContext> OnDeath;
		public event UnityAction<bool> OnStunStateChange;
			
		private CountdownTimer regenTimer;
		private CountdownTimer regenDelayTimer;
		private CountdownTimer stunTimer;
		private CountdownTimer damageCooldownTimer;
		
		public bool IsStunned { get; private set; }
		public bool CanTakeDamage { get; private set; }
		public bool InFinalStand => health.FinalStand && usedFinalStand;
		
		protected readonly List<Timer> timers = new List<Timer>();

		public float MaxHealth => health.MaxHealth(statsHandler);
		public float CurrentOvercharge => health.Overcharge;
		public float CurrentHealth => health.Current;

		public GameObject GameObject { get; private set; }

		private IStatsHandler statsHandler;
		private bool usedFinalStand;
		
		public void Initialize(HealthData newHealth, GameObject owner) {
			health = newHealth;
			
			GameObject = owner;
			if (owner && owner.TryGetComponent(out ICharacter character)) {
				statsHandler = character.Stats;
			}
			
			SetupTimers();
			health.Current = MaxHealth;
			CanTakeDamage = true;
		}
		
		public void Update() {
			foreach (Timer timer in timers) timer.Tick(Time.deltaTime);
		}
		
		private void SetupTimers() {
			if (regenTimer == null) {
				regenTimer = new CountdownTimer(health.RegenTime);
				regenTimer.OnTimerTick += UpdateRegen;
				regenTimer.OnTimerComplete += CompleteRegen;
				timers.Add(regenTimer);
			}
			else {
				regenTimer.SetInitialTime(health.RegenTime);
			}

			if (regenDelayTimer == null) {
				regenDelayTimer = new CountdownTimer(health.RegenDelayTime);
				regenDelayTimer.OnTimerComplete += StartRegen;
				timers.Add(regenDelayTimer);
			}
			else {
				regenDelayTimer.SetInitialTime(health.RegenDelayTime);
			}

			if (stunTimer == null) {
				if (!(health.StunDuration >= 0)) return;
				
				stunTimer = new CountdownTimer(health.StunDuration);
				stunTimer.OnTimerComplete += RecoverFromStun;
				timers.Add(stunTimer);
			}
			else {
				stunTimer.SetInitialTime(health.StunDuration);
			}

			if (damageCooldownTimer == null && health.HasDamageCooldown) {
				damageCooldownTimer = new CountdownTimer(health.DamageCooldownDuration);
				damageCooldownTimer.OnTimerComplete += () => CanTakeDamage = true;
				timers.Add(damageCooldownTimer);
			}
		}

		public bool IsAlive() {
			return health.Current > 0;
		}
		
		public void Die(DamageContext damageContext) {
			health.Current = 0;
			OnDeath?.Invoke(damageContext);
			stunTimer?.Stop();
		}
		
		public void TakeDamage(DamageContext ctx) {
			if (health.Invincible || !CanTakeDamage) return;
			if (IsStunned) return;
			if (health.Current <= 0) return;
			
			regenTimer.Stop();
			regenDelayTimer.Restart();

			float leftOverDamage = ctx.Damage;
			if (CurrentOvercharge > 0) {
				if (CurrentOvercharge >= leftOverDamage) {
					health.Overcharge -= leftOverDamage;
					leftOverDamage = 0;
				}
				else {
					leftOverDamage -= CurrentOvercharge;
					health.Overcharge = 0;
				}
			}

			if (leftOverDamage > 0) {
				health.Current = Mathf.Max((health.Current) - leftOverDamage, 0);
			}

			if (health.FinalStand && health.Current <= 1 && !usedFinalStand) {
				usedFinalStand = true;
				health.Current = 1;
			}

			if (health.HasDamageCooldown) {
				CanTakeDamage = false;
				damageCooldownTimer?.Restart();	
			}
			
			OnDamageTaken?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand });
			
			if (health.Current <= 0) {
				if (health.StunOnDeath) {
					health.Current = 1;
					Stun();
				}
				else {
					Die(ctx);
				}
			}
			else if (health.Current < health.StunThreshold) Stun();
		}

		public void AddOvercharge(float overcharge) {
			health.Overcharge += overcharge;
			OnHeal?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand});
		}
		
		public void Heal(float healAmount) {
			if (!health.CanHeal) return;

			health.Current = Mathf.Min(health.Current + healAmount, MaxHealth);
			OnHeal?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand});
		}

		public void ForceStun() {
			if (health.Current > health.StunThreshold) {
				health.Current = health.StunThreshold;
				OnDamageTaken?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand});	
			}

			Stun();
		}
		
		private void Stun() {
			if (IsStunned) return;
			IsStunned = true;
			stunTimer?.Restart();
			OnStunStateChange?.Invoke(true);
		}

		private void RecoverFromStun() {
			IsStunned = false;
			Heal(health.StunRecoveryHealth);
			OnStunStateChange?.Invoke(false);
		}
		
		public void StartRegen() {
			if (!health.CanRegen) return;

			regenTimer.StartAt(1 - health.Current / MaxHealth);
			OnRegenStart?.Invoke();
		}

		private void UpdateRegen() {
			health.Current = Mathf.Lerp(0, MaxHealth, 1 - regenTimer.Progress);
			OnRegenUpdate?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand});
		}

		private void CompleteRegen() {
			health.Current = MaxHealth;
			OnRegenComplete?.Invoke(new HealthContext { Current = CurrentHealth,  Max = MaxHealth, Overcharge = CurrentOvercharge, FinalStand = InFinalStand});
		}
	}
}