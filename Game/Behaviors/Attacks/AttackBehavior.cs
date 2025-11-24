namespace PolyWare.Game {
	public abstract class AttackBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class AttackBehavior : Behavior {
		protected AttackBehavior(ICharacter parent) : base(parent) {
			if (parent.GameObject.TryGetComponent(out IDamageable damageable)) {
				damageable.OnStunStateChange += OnStun;
			}
		}
		public abstract void OnPlayerHit(ICharacter player);

		protected abstract void OnStun(bool isStunned);
	}
}