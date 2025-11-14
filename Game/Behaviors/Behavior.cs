namespace PolyWare.Game {
	public abstract class Behavior : IBehavior {
		public bool IsRunning { get; private set; }
		protected readonly ICharacter parent;
		
		protected Behavior(ICharacter parent) {
			this.parent = parent;
		}

		public void Start() {
			IsRunning = true;
			OnStart();
		}
		
		public void Tick(float dt) {
			OnTick(dt);	
		}

		public void Complete() {
			OnComplete();
			IsRunning = false;
		}
		
		protected abstract void OnStart();
		protected abstract void OnTick(float dt);
		protected abstract void OnComplete();
	}
}