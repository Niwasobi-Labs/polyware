using System;
using UnityEngine;

namespace PolyWare.Game {
	public interface IDamageable {
		
		event Action<DamageContext> OnDeath;
		event Action<DamageContext> OnHit;
		event Action OnStun;
		
		public GameObject GameObject { get; } // todo: replace with Entity (https://app.clickup.com/t/86b6vbg95)
		public void TakeDamage(DamageContext damageContext);
		public void Stun();
		public bool IsStunned { get; }
		public void Heal(float healAmount);
		public bool IsAlive();
		public void Die(DamageContext damageContext);
	}
}