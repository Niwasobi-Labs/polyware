namespace PolyWare.Game {
	public abstract class MovementBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class MovementBehavior : IBehavior {
		protected readonly ICharacter parent;
		
		protected MovementBehavior(ICharacter character) {
			parent = character;
		}
		
		public abstract void Start();
		public abstract void Tick(float dt);
		public abstract void Complete();
	}
}