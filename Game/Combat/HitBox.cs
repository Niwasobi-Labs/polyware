using PolyWare.Core;
using System;
using UnityEngine;

namespace PolyWare.Game {
	public class HitBox : MonoBehaviour, IDamageable, IAffectable {
		[SerializeField] private GameObject owner;

		public event Action<DamageContext> OnDeath;
		public event Action<DamageContext> OnHit;
		public event Action OnStun;
		public GameObject GameObject => owner;
		
		private IDamageable ownerDamageable;
		private IAffectable ownerAffectable;

		public void Awake() {
			if (!owner.TryGetComponent(out ownerDamageable)) {
				Log.Error($"Hitbox needs an owner who is damageable");
			}
			
			if (!owner.TryGetComponent(out ownerAffectable)) {
				Log.Error($"Hitbox needs an owner who is affectable");
			}
		}

		public void TakeDamage(DamageContext damageContext) {
			OnHit?.Invoke(damageContext);
			ownerDamageable.TakeDamage(damageContext);
		}

		public void Stun() {
			OnStun?.Invoke();
			ownerDamageable.Stun();
		}

		public bool IsStunned => ownerDamageable.IsStunned;

		public void Heal(float healAmount) => ownerDamageable.Heal(healAmount);
		public void Affect(IEffect effect, ContextHolder ctx) => ownerAffectable.Affect(effect, ctx); 
		public bool IsAlive() => ownerDamageable.IsAlive();
		public void Kill(DamageContext damageContext) {
			ownerDamageable.Kill(damageContext);
			OnDeath?.Invoke(damageContext);
		}
	}
}