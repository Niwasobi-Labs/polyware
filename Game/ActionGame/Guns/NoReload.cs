namespace PolyWare.ActionGame.Guns {
	public class NoReload : ReloadHandler {
		public override bool IsPreventingUse => false;
		public override bool CanReload => false;
		
		public NoReload(Gun gun) : base(gun) { }
	}
}