namespace PolyWare.Timers {
	public class Stopwatch : Timer {
		public Stopwatch() : base(0) { }
		
		public override void Tick(float deltaTime) {
			base.Tick(deltaTime);
			if (IsRunning) {
				Time += deltaTime;
			}
		}
	}
}