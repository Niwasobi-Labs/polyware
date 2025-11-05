namespace PolyWare.Game {
	public abstract class AttackBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class AttackBehavior : IBehavior {
		protected readonly ICharacter parent;
		
		protected AttackBehavior(ICharacter character) {
			parent = character;
		}
		
		public abstract void Start();
		public abstract void Tick(float dt);
		public abstract void OnPlayerHit(ICharacter player);
		public abstract void Complete();
	}
}