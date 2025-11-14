namespace PolyWare.Game {
	public interface IBehavior {
		public void Start();
		public void Tick(float dt);
		public void Complete();
		public bool IsRunning { get; }
	}

	public interface IBehaviorFactory {
		IBehavior Create(ICharacter parent);
	}
}