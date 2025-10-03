using System;
using System.Data.Common;
using PolyWare.Core;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Combat {
	public class HitBox : MonoBehaviour, IDamageable, IAffectable {
		[SerializeField] private GameObject owner;

		public event Action<DamageContext> OnDeath;
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

		public void TakeDamage(DamageContext damageContext) => ownerDamageable.TakeDamage(damageContext);
		public void Heal(float healAmount) => ownerDamageable.Heal(healAmount);
		public void Affect(IEffect effect, ContextHolder ctx) => ownerAffectable.Affect(effect, ctx); 
		public bool IsAlive() => ownerDamageable.IsAlive();
		public void Die(DamageContext damageContext) {
			OnDeath?.Invoke(damageContext);
			ownerDamageable.Die(damageContext);
		}
	}
}