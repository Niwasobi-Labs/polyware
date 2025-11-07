using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class GunSpawnData : WeaponSpawnData {
		[Tooltip("-1 will default to the MaxAmmo")]
		public int StartingClipAmmo = -1;
		[Tooltip("-1 will default to the MaxReserveAmmo")]
		public int StartingReserveAmmo = -1;
	}
	
	[Serializable]
	public class GunData : WeaponData {
		public GunData(EquipmentDefinition definition) : base(definition) { }
		
		public GunDefinition GunDefinition => Definition as GunDefinition;

		public int CurrentAmmo;
		public int ReserveAmmo;
		public float CurrentHeat; // todo: make this private and use events to update UI
		
		public override void Override(IEntitySpawnData data) {
			base.Override(data);
			
			var spawnData = data as GunSpawnData;
			CurrentAmmo = spawnData.StartingClipAmmo < 0 ? GunDefinition.MaxAmmo : spawnData.StartingClipAmmo;
			ReserveAmmo = spawnData.StartingReserveAmmo < 0 ? GunDefinition.MaxReserveAmmo : spawnData.StartingReserveAmmo;
		}
		
		public void SetCurrentAmmo(int ammo) {
			CurrentAmmo = GunDefinition.BottomlessClip ? GunDefinition.MaxAmmo : ammo;
			RaiseDataChangedEvent();
		}
		
		public void SetReserveAmmo(int ammo) {
			ReserveAmmo = GunDefinition.InfiniteAmmo ? int.MaxValue : ammo;
			RaiseDataChangedEvent();
		}
		
		public bool NeedsAmmo() {
			return ReserveAmmo < GunDefinition.MaxReserveAmmo;
		}
		
		public void RefillReserves() {
			ReserveAmmo = GunDefinition.MaxReserveAmmo;
			RaiseDataChangedEvent();
		}
		
		public int AddAmmoToReserves(int ammo) {
			int canAdd = Mathf.Min(ammo, GunDefinition.MaxReserveAmmo - ReserveAmmo);
			SetReserveAmmo(ReserveAmmo + canAdd);
			return ammo - canAdd;
		}
	}
}