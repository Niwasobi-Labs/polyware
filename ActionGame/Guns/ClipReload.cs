namespace PolyWare.ActionGame.Guns {
	public class ClipReload : ReloadHandler {
		
		public ClipReload(Gun gun) : base(gun) { }
		public override bool IsPreventingUse => IsReloading;
		public override bool CanReload => !timer.IsRunning;
		
		protected override void OnComplete() {
			int neededAmmo = gun.GunData.GunDefinition.MaxAmmo - gun.GunData.CurrentAmmo;

			if (gun.GunData.ReserveAmmo < neededAmmo) {
				gun.GunData.SetCurrentAmmo(gun.GunData.ReserveAmmo);
				gun.GunData.SetReserveAmmo(0);
			}
			else {
				gun.GunData.SetReserveAmmo(gun.GunData.ReserveAmmo - neededAmmo);
				gun.GunData.SetCurrentAmmo(gun.GunData.GunDefinition.MaxAmmo);
			}

			base.OnComplete();
		}
	}
}