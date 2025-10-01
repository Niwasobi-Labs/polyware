using System;
using PolyWare.Abilities;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Combat {
	public class HitBox : MonoBehaviour, IDamageable {
		[SerializeField] private GameObject owner;

		public GameObject GameObject => owner;
		private IDamageable ownerDamageable;

		public void Awake() {
			if (!owner.TryGetComponent(out ownerDamageable)) {
				Log.Error($"Hitbox needs an owner who is damageable");
			}
		}

		public void TakeDamage(DamageInfo damageInfo) => ownerDamageable.TakeDamage(damageInfo);
		public void Heal(float healAmount) => ownerDamageable.Heal(healAmount);
		public void ApplyEffect(IEffect<IDamageable> effect, AbilityContext context) => ownerDamageable.ApplyEffect(effect, context);
		public bool IsAlive() => ownerDamageable.IsAlive();
		public void Die() => ownerDamageable.Die();
	}
}