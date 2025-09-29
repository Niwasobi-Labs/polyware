using UnityEngine;

namespace PolyWare.Combat {
	public class HitBox : MonoBehaviour, IDamageable {
		[SerializeField] private Health owner;

		public GameObject GameObject => owner.GameObject;
		public void TakeDamage(DamageInfo damageInfo) => owner.TakeDamage(damageInfo); 
		public bool IsAlive() => owner.IsAlive();
		public void Die() => owner.Die();
	}
}