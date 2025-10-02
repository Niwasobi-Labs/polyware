using System;
using PolyWare.Abilities;
using PolyWare.Core;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Combat {
	public class HitBox : MonoBehaviour, IDamageable, IAffectable {
		[SerializeField] private GameObject owner;

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

		public void TakeDamage(DamageInfo damageInfo) => ownerDamageable.TakeDamage(damageInfo);
		public void Heal(float healAmount) => ownerDamageable.Heal(healAmount);
		public void Affect(IEffect effect, ContextHolder ctx) => ownerAffectable.Affect(effect, ctx); 
		public bool IsAlive() => ownerDamageable.IsAlive();
		public void Die() => ownerDamageable.Die();
	}
}