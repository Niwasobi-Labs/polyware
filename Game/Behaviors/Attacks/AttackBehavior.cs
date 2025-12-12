namespace PolyWare.Game {
	public abstract class AttackBehaviorFactory : BehaviorFactory {
		
	}
	
	public abstract class AttackBehavior : Behavior {
		protected AttackBehavior(ICharacter parent, AttackBehaviorFactory factory) : base(parent, factory) {
			if (parent.GameObject.TryGetComponent(out IDamageable damageable)) {
				damageable.OnStunStateChange += OnStun;
			}
		}
		
		public abstract void OnPlayerHit(ICharacter player);
		protected abstract void OnStun(bool isStunned);
	}
}