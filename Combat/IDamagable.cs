using PolyWare.Abilities;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Combat {
	public interface IDamageable {
		public GameObject GameObject { get; } // todo: replace with Entity (https://app.clickup.com/t/86b6vbg95)
		public void TakeDamage(DamageInfo damageInfo);
		public void Heal(float healAmount);
		public void ApplyEffect(IEffect<IDamageable> effect, AbilityContext context);
		public bool IsAlive();
		public void Die();
	}
}