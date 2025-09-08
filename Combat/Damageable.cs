using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Combat {
	public class Damageable : MonoBehaviour, IDamageable {
		
		[InfoBox("When objects are spawned, these values can often be overridden via Component Data or SpawnOverrideData. " +
		         "Toggle this on if you want to block that from happening, and only ever use what this component has outlined.")]
		public bool UseComponentDefaultsOnly = false; 
		
		[field: ShowIf("UseComponentDefaultsOnly")] [field: SerializeField] public DamageableValueHandler.DamageValueHandlerData HealthInfo { get; private set; }
		[field: ShowIf("UseComponentDefaultsOnly")] [field: SerializeField] public DamageableValueHandler.DamageValueHandlerData ShieldInfo { get; private set; }
		
		[ShowInInspector] [FoldoutGroup("Debug Info")] public DamageableValueHandler Health { get; private set; }
		[ShowInInspector] [FoldoutGroup("Debug Info")] public DamageableValueHandler Shield { get; private set; }
		
		public event UnityAction<float, float> OnDamageTaken = delegate {};
		public event UnityAction OnDeath = delegate {};

		// todo: clean up awake vs init flow 
		private void Awake() {
			Health = new DamageableValueHandler(HealthInfo);
			Shield = new DamageableValueHandler(ShieldInfo);
			
			Health.OnValueDepleted += Die;
		}
		
		public void InitializeData(DamageableValueHandler.DamageValueHandlerData healthData, DamageableValueHandler.DamageValueHandlerData shieldData) {
			if (UseComponentDefaultsOnly) return;
			
			Health = new DamageableValueHandler(healthData);
			Shield = new DamageableValueHandler(shieldData);
			
			Health.OnValueDepleted += Die;
		}
		
		private void Update() {
			Health.TickTimers(Time.deltaTime);
			Shield.TickTimers(Time.deltaTime);
		}
		
		public void TakeDamage(DamageInfo damageInfo) {
			if (Shield.Current > 0) {
				Shield.TakeDamage(damageInfo.Damage);

				if (Shield.Current <= 0 && damageInfo.PenetrateShields) Health.TakeDamage(damageInfo.Damage);
			}
			else {
				Health.TakeDamage(damageInfo.Damage);
			}
			
			OnDamageTaken.Invoke(Health.Current / Health.Max, Shield.Current / Shield.Max);
		}
		
		public bool IsAlive() {
			return Health.Current > 0;
		}
		
		public void Die() {
			OnDeath.Invoke();
		}

		public void HealHealth(float healAmount) {
			Health.Heal(healAmount);
		}

		public void HealShields(float healAmount) {
			Shield.Heal(healAmount);
		}
	}
}