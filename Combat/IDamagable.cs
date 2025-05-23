namespace PolyWare.Combat {
	public interface IDamageable {
		public void TakeDamage(DamageInfo damageInfo);
		public bool IsAlive();
		public void Die();
	}
}