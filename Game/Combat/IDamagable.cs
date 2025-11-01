using System;
using UnityEngine;

namespace PolyWare.Game {
	public interface IDamageable {
		
		event Action<DamageContext> OnDeath;
		event Action<DamageContext> OnHit;
		
		public GameObject GameObject { get; } // todo: replace with Entity (https://app.clickup.com/t/86b6vbg95)
		public void TakeDamage(DamageContext damageContext);
		public void Heal(float healAmount);
		public bool IsAlive();
		public void Die(DamageContext damageContext);
	}
}