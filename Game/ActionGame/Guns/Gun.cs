using System.Collections.Generic;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;

// todo: what if we wanted to use a Gun without a character? Like a turret?
namespace PolyWare.Game {
	public class Gun : Weapon {

		[ShowInInspector, Sirenix.OdinInspector.ReadOnly]
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
		private CountdownTimerWithPredicate fireRate;
		private CountdownTimer overheatTimer;
		
		protected override void OnInitialize() {
			aimAssistStrategy = AimAssistStrategy.Create(GunData.GunDefinition.AimAssist, bulletSpawn);
			
			if (GunData.CurrentAmmo <= 0) GunData.SetCurrentAmmo(GunData.GunDefinition.MaxAmmo);
			if (GunData.ReserveAmmo <= 0) GunData.AddAmmoToReserves(GunData.GunDefinition.MaxReserveAmmo);
			
			SetupReload();
			
			laserSight?.SetRange(GunData.GunDefinition.Range * GunData.GunDefinition.RangeOfLaserSight);
			
			fireRate = new CountdownTimerWithPredicate(GunData.GunDefinition.FireRate, CheckAdditionalFireRateLogic);

			overheatTimer = new CountdownTimer(GunData.GunDefinition.OverHeatTime);
			overheatTimer.OnTimerTick += OnOverheatUpdate;
			overheatTimer.OnTimerComplete += Cooldown;

			timers = new List<Timer>(3) { fireRate, overheatTimer };
		}

		private void SetupReload() {
			ReloadHandler = GunData.GunDefinition.ReloadType switch {
				ReloadStrategy.None => new NoReload(this),
				ReloadStrategy.Clip => new ClipReload(this),
				ReloadStrategy.OneByOne => new OneByOneReload(this),
				_ => throw new System.ArgumentOutOfRangeException()
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

		public override bool CanUse => GunData.CurrentAmmo > 0 && !ReloadHandler.IsPreventingUse && !fireRate.IsRunning && !overheatTimer.IsRunning;

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

			FireProjectiles();
			
			GunData.SetCurrentAmmo(GunData.CurrentAmmo - GunData.GunDefinition.AmmoConsumptionPerShot);

			fireRate.SetInitialTime(GunData.GunDefinition.FireRateEvaluator.Evaluate(myCharacter?.Stats, GunData.GunDefinition.FireRate));
			fireRate.Start();

			if (GunData.GunDefinition.CanOverheat) AddHeat(GunData.GunDefinition.HeatPerShot);

			if (GunData.CurrentAmmo <= 0 && CanReload()) StartReload();

			ServiceLocator.Global.Get<IAudioService>().PlayOneShot(GunData.GunDefinition.ShootingSound, transform.position);
			
			TriggerOnSuccessfulUseAbility();
			
			//if (myCharacter.IsPlayer) PlayerCharacter.OnPlayerFired?.Invoke(GunData.GunDefinition.AmmoConsumptionPerShot); task: use event bus
		}

		private void TriggerOnSuccessfulUseAbility() {
			if (GunData.GunDefinition.OnSuccessfulUseAbility == null) return;
			
			Ability ability = GunData.GunDefinition.OnSuccessfulUseAbility.CreateInstance();
			AbilityContextHolder abilityCtxHolder = new AbilityContextHolder (
				GunData.GunDefinition.OnSuccessfulUseAbility, 
				myCharacter.GameObject, 
				new List<GameObject>{ aimAssistStrategy.GetTargetTransform()?.gameObject },
				new List<IContext> {
					GunData, 
					new DamageContext(GunData.GunDefinition.BulletDamageEvaluator.Evaluate(myCharacter?.Stats, GunData.GunDefinition.Damage), myCharacter?.Transform.gameObject, GunData.GunDefinition.FireAbility),
					myCharacter?.FactionMember.FactionInfo
				}
			);
			ability.Trigger(abilityCtxHolder);
		}
		
		private void FireProjectiles() {
			var abilityCtxHolder = new AbilityContextHolder(
				GunData.GunDefinition.FireAbility,
				myCharacter.Transform.gameObject,
				null,
				new List<IContext> {
					GunData,
					new DamageContext(GunData.GunDefinition.BulletDamageEvaluator.Evaluate(myCharacter?.Stats, GunData.GunDefinition.Damage), myCharacter?.Transform.gameObject, GunData.GunDefinition.FireAbility),
					myCharacter?.FactionMember.FactionInfo
				});
			
			ProjectileSpawnStrategy.Spawn(new ProjectileSpawnContext(
				CreateProjectileData(),
				GunData.GunDefinition.BulletCountPerShot,
				bulletSpawn.position, 
				CalculateProjectileDirection(), 
				bulletSpawn.up,
				GunData.GunDefinition.Spread,
				GunData.GunDefinition.SpreadOffset, 
				abilityCtxHolder));
		}

		private ProjectileData CreateProjectileData() {
			return new ProjectileData(
				GunData.GunDefinition.Bullet,
				myCharacter.FactionMember.FactionInfo,
				aimAssistStrategy.GetTargetTransform(),
				GunData.GunDefinition.BulletSpeed);
		}
		
		public Vector3 CalculateProjectileDirection() {
			float spread = GunData.GunDefinition.Spread;
			Vector3 direction = aimAssistStrategy.CalculateAdjustedDirectionToTarget();
			if (!(spread > 0f) || !GunData.GunDefinition.RandomSpreadVariance) return direction.normalized;
			
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