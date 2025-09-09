using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.ActionGame.Guns {
	public class ScatterGun : Gun {
		protected override void SpawnProjectile() {

			float angleStep = GunData.GunDefinition.Spread / (GunData.GunDefinition.AmmoConsumptionPerShot - 1);
			float startingAngle = -(GunData.GunDefinition.Spread / 2f);
			
			for (int i = 0; i < GunData.GunDefinition.AmmoConsumptionPerShot; i++) {
				float angle = startingAngle + angleStep * i;
				CreateProjectile(bulletSpawn.position, Quaternion.AngleAxis(angle, bulletSpawn.up) * bulletSpawn.forward);
			}
		}
	}
}