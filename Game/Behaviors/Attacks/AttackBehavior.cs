namespace PolyWare.Game {
	public abstract class AttackBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class AttackBehavior : Behavior {
		protected AttackBehavior(ICharacter parent) : base(parent) { }
		public abstract void OnPlayerHit(ICharacter player);
	}
}