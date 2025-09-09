using System;
using System.Collections.Generic;
using PolyWare.ActionGame.AimAssist;
using PolyWare.ActionGame.Projectiles;
using PolyWare.Combat;
using PolyWare.Debug;
using PolyWare.Timers;
using Sirenix.OdinInspector;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

// todo: what if we wanted to use a Gun without a character? Like a turret?
namespace PolyWare.ActionGame.Guns {
	public class Gun : Weapon {

		[ShowInInspector, ReadOnly]
		public GunData GunData => Data as GunData;
		
		[Title("Gun")]
		[SerializeField] protected Transform bulletSpawn;
		[SerializeField] private LaserSight laserSight;
		
		private bool triggerPulled;
		private bool isAiming;
		
		//task: overheating no longer works (needs to be moved to GunHeatHandler)
		//private PlayerGunHeatEvent heatEvent;
		
		private AimAssistStrategy aimAssistStrategy;
		public ReloadHandler ReloadHandler { get; private set; }
		
		private List<Timer> timers;
		private CountdownWithPredicateTimer fireRateTimer;
		private CountdownTimer overheatTimer;
		
		protected override void OnInitialize() {
			aimAssistStrategy = AimAssistStrategy.Create(GunData.GunDefinition.AimAssist, bulletSpawn);
			
			if (GunData.CurrentAmmo <= 0) GunData.SetCurrentAmmo(GunData.GunDefinition.MaxAmmo);
			if (GunData.ReserveAmmo <= 0) GunData.AddAmmoToReserves(GunData.GunDefinition.MaxReserveAmmo);
			
			SetupReload();
			
			laserSight?.SetRange(GunData.GunDefinition.Range * GunData.GunDefinition.RangeOfLaserSight);
			
			fireRateTimer = new CountdownWithPredicateTimer(GunData.GunDefinition.FireRate, CheckAdditionalFireRateLogic);

			overheatTimer = new CountdownTimer(GunData.GunDefinition.OverHeatTime) {
				OnTimerTick = OnOverheatUpdate,
				OnTimerComplete = Cooldown
			};

			timers = new List<Timer>(3) { fireRateTimer, overheatTimer };
		}

		private void SetupReload() {
			ReloadHandler = GunData.GunDefinition.ReloadType switch {
				ReloadStrategy.None => new NoReload(this),
				ReloadStrategy.Clip => new ClipReload(this),
				ReloadStrategy.OneByOne => new OneByOneReload(this),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
		
		private void Update() {
			foreach (Timer timer in timers) { timer.Tick(Time.deltaTime); }
			
			ReloadHandler.Update(Time.deltaTime);
			
			if (!overheatTimer.IsRunning && GunData.CurrentHeat > 0) UpdateHeatCooldown();
			
			aimAssistStrategy.RunAimAssist();
			
			if (triggerPulled && CanUse) Shoot();
		}

		protected override void OnEquip() {
			if (GunData.CurrentAmmo == 0) StartReload();
		}

		protected override bool OnUnequip() {
			StopUsing();
			
			if (ReloadHandler.IsReloading) ReloadHandler.Cancel();
			
			foreach (Timer timer in timers) { timer.Stop(); }

			return true;
		}

		public override bool CanUse => GunData.CurrentAmmo > 0 && !ReloadHandler.IsPreventingUse && !fireRateTimer.IsRunning && !overheatTimer.IsRunning;

		public override void Use() {
			triggerPulled = true;
		}
		public override void StopUsing() {
			triggerPulled = false;
		}
		
		#region Shooting
		
		private void Shoot() {
			// if we are reloading but have gotten into this function, that means the reload can be interrupted
			if (ReloadHandler.IsReloading) ReloadHandler.Cancel();
			
			SpawnProjectile();
			
			GunData.SetCurrentAmmo(GunData.CurrentAmmo - GunData.GunDefinition.AmmoConsumptionPerShot);

			fireRateTimer.Start();

			if (GunData.GunDefinition.CanOverheat) AddHeat(GunData.GunDefinition.HeatPerShot);

			if (GunData.CurrentAmmo <= 0 && CanReload()) StartReload();

			PolyWare.Core.Instance.SfxManager.PlayClip(GunData.GunDefinition.ShootingSfx, transform);
			
			//if (myCharacter.IsPlayer) PlayerCharacter.OnPlayerFired?.Invoke(GunData.GunDefinition.AmmoConsumptionPerShot); task: use event bus
		}

		protected Projectile CreateProjectile(Vector3 position, Vector3 direction) {
			Projectile newProjectile = Instantiate(GunData.GunDefinition.BulletPrefab, position, Quaternion.Euler(direction)).GetComponent<Projectile>();
			newProjectile.Initialize(new DamageInfo(myCharacter.IsPlayer, GunData.GunDefinition.Damage), GunData.GunDefinition.BulletSpeed, direction, aimAssistStrategy.GetTargetTransform());
			return newProjectile;
		}
		
		protected virtual void SpawnProjectile() {
			CreateProjectile(bulletSpawn.position, CalculateProjectileDirection(GunData.GunDefinition.Spread));
		}

		protected Vector3 CalculateProjectileDirection(float spread) {
			Vector3 direction = aimAssistStrategy.CalculateAdjustedDirectionToTarget();
			if (!(spread > 0f)) return direction.normalized;
			
			float angle = UnityEngine.Random.Range(-spread * 0.5f, spread * 0.5f);
			Vector3 axis = Vector3.Cross(direction, Vector3.up).normalized;
			direction = Quaternion.AngleAxis(angle, axis) * direction;

			angle = UnityEngine.Random.Range(-spread * 0.5f, spread * 0.5f);
			direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;

			return Vector3.ProjectOnPlane(direction, bulletSpawn.up).normalized;
		}
		
		private bool CheckAdditionalFireRateLogic() {
			return GunData.GunDefinition.FiringMode != GunDefinition.FireMode.SemiAuto || !triggerPulled;
		}
		
		#endregion
		
		#region Overheating
		
		// todo: move heating out of here, and get events out of here as well (GunHeatHandler?)
		private void AddHeat(float heatToAdd) {
			GunData.CurrentHeat += heatToAdd;
			if (GunData.CurrentHeat >= GunData.GunDefinition.MaxHeat) Overheat();
			//else if (myCharacter is PlayerCharacter) EventBus<PlayerGunHeatEvent>.Raise(heatEvent.Set(GunData.CurrentHeat / GunData.GunDefinition.MaxHeat, PlayerGunHeatEvent.HeatState.Heating));
		}

		private void UpdateHeatCooldown() {
			GunData.CurrentHeat -= Mathf.Max(GunData.GunDefinition.HeatCooldownRate * Time.deltaTime, 0f);
			//if (myCharacter is PlayerCharacter) EventBus<PlayerGunHeatEvent>.Raise(heatEvent.Set(GunData.CurrentHeat / GunData.GunDefinition.MaxHeat, PlayerGunHeatEvent.HeatState.Heating));
		}
		
		public void Overheat() {
			overheatTimer.Start();
			//if (myCharacter is PlayerCharacter) EventBus<PlayerGunHeatEvent>.Raise(heatEvent.Set(1, PlayerGunHeatEvent.HeatState.Overheating));
		}

		private void OnOverheatUpdate() {
			//if (myCharacter is PlayerCharacter) EventBus<PlayerGunHeatEvent>.Raise(heatEvent.Set(overheatTimer.Progress, PlayerGunHeatEvent.HeatState.Heating));
		}
		
		public void Cooldown() {
			GunData.CurrentHeat = 0f;
			//if (myCharacter is PlayerCharacter) EventBus<PlayerGunHeatEvent>.Raise(heatEvent.Set(0, PlayerGunHeatEvent.HeatState.Cooldown));
		}
		#endregion

		#region Reloading
		
		public bool CanReload() {
			return ReloadHandler.CanReload && !overheatTimer.IsRunning && GunData.CurrentAmmo < GunData.GunDefinition.MaxAmmo && GunData.ReserveAmmo > 0;
		}

		public void StartReload() {
			if (CanReload()) ReloadHandler.Start();
		}

		public void CancelReload() {
			ReloadHandler.Cancel();
		}
		#endregion
		
		public void SetLaserSightStatus(bool status) {
			if (laserSight) laserSight.SetStatus(status);
		}

		// task: guns no longer give ammo when walked over, move this out of here
		// public void GrantAmmoTo(IProximityUser user) {
		// 	if (!user.GetUserObject().TryGetComponent(out ICharacter character)) return;
		//
		// 	if (!player.Inventory.Weapons.NeedsAmmoForType(GunData.GunDefinition.ItemId)) return;
		//
		// 	int ammoLeft = player.Inventory.Weapons.AddAmmo(GunData.GunDefinition.ItemId, GunData.CurrentAmmo + GunData.ReserveAmmo);
		//
		// 	if (ammoLeft <= 0) {
		// 		Destroy(gameObject);
		// 		return;
		// 	}
		// 	
		// 	if (ammoLeft >= GunData.GunDefinition.MaxAmmo) {
		// 		GunData.SetCurrentAmmo(GunData.GunDefinition.MaxAmmo);
		// 		GunData.SetReserveAmmo(ammoLeft - GunData.GunDefinition.MaxAmmo);
		// 	} else {
		// 		GunData.SetCurrentAmmo(ammoLeft);
		// 		GunData.SetReserveAmmo(0);
		// 	}
		// }
		
		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying) return;
			
			aimAssistStrategy.DrawGizmos();
		}
	}
}