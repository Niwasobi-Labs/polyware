namespace PolyWare.ActionGame.Guns {
	public enum ReloadStrategy {
		None,
		Clip,
		OneByOne
	}
	
	public interface IReloadHandler {
		public bool IsReloading { get; }
		public bool IsPreventingUse { get; }
		public bool CanReload { get; }
		
		public void Start();
		public void Update(float deltaTime);
		public void Cancel();
	}
}