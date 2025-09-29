using System.Collections.Generic;
using PolyWare.Debug;
using PolyWare.Timers;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Combat {
	public class Health : MonoBehaviour, IDamageable {

		[Tooltip("These values can be overwritten by many systems")]
		[SerializeField] private HealthData health;

		public GameObject GameObject => gameObject;
		
		public event UnityAction<float> OnDamageTaken;
		public event UnityAction<float> OnHeal;
		public event UnityAction OnRegenStart;
		public event UnityAction<float> OnRegenUpdate;
		public event UnityAction<float> OnRegenComplete;
		public event UnityAction OnDeath;
			
		public CountdownTimer RegenTimer;
		public CountdownTimer RegenDelayTimer;

		protected readonly List<Timer> timers = new List<Timer>();

		public float Max => health.Max;
		public float Current => health.Current;

		private void Awake() {
			Initialize(health);
		}

		public void Initialize(HealthData newHealth) {
			health = newHealth;
			health.Current = newHealth.Max;
			SetupTimers();
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
		
		public void Die() {
			OnDeath?.Invoke();
		}
		
		public void TakeDamage(DamageInfo damageData) {
			if (health.Invincible) return;
			if (health.Current <= 0) return;
			
			RegenTimer.Stop();
			RegenDelayTimer.Restart();
			
			health.Current = Mathf.Max(health.Current - damageData.Damage, 0);

			OnDamageTaken?.Invoke(health.Current / health.Max);
			
			if (health.Current <= 0) Die();
		}
		
		public void Heal(float healAmount) {
			if (!health.CanHeal) return;
			
			health.Current = Mathf.Min(health.Current + healAmount, health.Max);
			OnHeal?.Invoke(health.Current / health.Max);
		}

		public void StartRegen() {
			if (!health.CanRegen) return;

			RegenTimer.StartAt(1 - health.Current / health.Max);
			OnRegenStart?.Invoke();
		}

		private void UpdateRegen() {
			health.Current = Mathf.Lerp(0, Max, 1 - RegenTimer.Progress);
			OnRegenUpdate?.Invoke(health.Current / health.Max);
		}

		private void CompleteRegen() {
			health.Current = Max;
			OnRegenComplete?.Invoke(health.Current / health.Max);
		}
	}
}