using System;
using PolyWare.Characters;
using PolyWare.Debug;
using PolyWare.Core.Entities;
using PolyWare.Interactions;
using PolyWare.Items;
using PolyWare.Timers;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	public abstract class PowerUp : Entity<PowerUpData>, IPickupable {
		protected ICharacter character;

		// todo: remove the user from Pickup and handle powerups through this auto-prop
		public bool AutoPickup => true;
		public override PowerUpData Data { get; protected set; }
		
		private CountdownTimer lifeTimer;

		protected override void OnInitialize() {
			lifeTimer = new CountdownTimer(Data.Definition.LifeTime) {
				OnTimerComplete = () => Destroy(gameObject)
			};
		}

		protected virtual void Start() {
			if (Data.Definition.LifeTime > 0) lifeTimer.Start();
		}

		protected void Update() {
			lifeTimer.Tick(Time.deltaTime);
		}

		public void Pickup(IProximityUser user) {
			if (!user.GetUserObject().TryGetComponent(out character)) {
				Log.Error($"Can't pickup { name } with {user.GetUserObject().name}");
				return;
			}
			
			// PolyWare.Core.Instance.SfxManager.PlayClip(Data.PickupSound, transform);
			OnUse();
		}

		public abstract void OnUse();
	}
}