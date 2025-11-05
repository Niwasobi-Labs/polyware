namespace PolyWare.Game {
	public abstract class DeathBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class DeathBehavior : IBehavior {
		protected readonly ICharacter parent;
		
		protected DeathBehavior(ICharacter character) {
			parent = character;
		}
		
		public abstract void Start();
		public abstract void Tick(float dt);
		public abstract void Complete();
	}
}