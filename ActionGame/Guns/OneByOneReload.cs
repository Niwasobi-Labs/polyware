namespace PolyWare.ActionGame.Guns {
	public class OneByOneReload : ReloadHandler {
		
		public OneByOneReload(Gun gun) : base(gun) { }
		public override bool IsPreventingUse => false;
		public override bool CanReload => !timer.IsRunning;
		
		protected override void OnComplete() {
			
			if (gun.GunData.ReserveAmmo < gun.GunData.GunDefinition.AmmoPerReload) {
				gun.GunData.SetCurrentAmmo(gun.GunData.CurrentAmmo + gun.GunData.ReserveAmmo);
				gun.GunData.SetReserveAmmo(0);
			}
			else {
				gun.GunData.SetReserveAmmo(gun.GunData.ReserveAmmo - gun.GunData.GunDefinition.AmmoPerReload);
				gun.GunData.SetCurrentAmmo(gun.GunData.CurrentAmmo + gun.GunData.GunDefinition.AmmoPerReload);
			}

			base.OnComplete();
			
			if (gun.GunData.ReserveAmmo == 0 || gun.GunData.CurrentAmmo == gun.GunData.GunDefinition.MaxAmmo) return;

			Start();
		}
	}
}