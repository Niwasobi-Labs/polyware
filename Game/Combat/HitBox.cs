using PolyWare.Core;
using System;
using UnityEngine;

namespace PolyWare.Game {
	public class HitBox : MonoBehaviour, IDamageable, IAffectable {
		[SerializeField] private GameObject owner;

		public event Action<DamageContext> OnDeath;
		public event Action<DamageContext> OnHit;
		public event Action<bool> OnStunStateChange;
		public GameObject GameObject => owner;
		
		private IDamageable ownerDamageable;
		private IAffectable ownerAffectable;

		public void Awake() {
			if (!owner.TryGetComponent(out ownerDamageable)) {
				Log.Error($"Hitbox needs an owner who is damageable");
			}
			
			// not all hitboxes are affectable, that's ok
			owner.TryGetComponent(out ownerAffectable);
		}

		public void TakeDamage(DamageContext damageContext) {
			OnHit?.Invoke(damageContext);
			ownerDamageable.TakeDamage(damageContext);
		}

		public void Stun() {
			OnStunStateChange?.Invoke(true);
			ownerDamageable.Stun();
		}

		public float CurrentHealthPercentage => ownerDamageable.CurrentHealthPercentage;
		public bool IsStunned => ownerDamageable.IsStunned;

		public void Heal(float healAmount) => ownerDamageable.Heal(healAmount);
		public void AddOvercharge(float overcharge) => ownerDamageable.AddOvercharge(overcharge);
		
		public void Affect(IEffect effect, ContextHolder ctx) {
			ownerAffectable?.Affect(effect, ctx);
		}

		public void RemoveEffect(IEffect effect) {
			ownerAffectable?.RemoveEffect(effect);
		}

		public bool IsAlive() => ownerDamageable.IsAlive();
		public void Kill(DamageContext damageContext) {
			ownerDamageable.Kill(damageContext);
			OnDeath?.Invoke(damageContext);
		}
	}
}