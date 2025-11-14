namespace PolyWare.Game {
	public abstract class DeathBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class DeathBehavior : Behavior {
		protected DeathBehavior(ICharacter parent) : base(parent) { }
	}
}