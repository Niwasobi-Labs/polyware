using PolyWare.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class PowerUp : Entity<PowerUpData>, IPickupable {
		protected ICharacter character;

		// todo: remove the user from Pickup and handle power-ups through this auto-prop
		public bool AutoPickup => true;
		public override PowerUpData Data { get; protected set; }
		
		private CountdownTimer lifeTimer;

		protected override void OnInitialize() {
			lifeTimer = new CountdownTimer(Data.Definition.LifeTime);
			lifeTimer.OnTimerComplete += () => Destroy(gameObject);
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
			
			ServiceLocator.Global.Get<IAudioService>().PlayOneShot(Data.Definition.PickupSound, transform.position);
			var newAbility = Data.Definition.OnPickupAbility.CreateInstance();
			newAbility.Trigger(new AbilityContextHolder(Data.Definition.OnPickupAbility, gameObject, new List<GameObject>{ character.Transform.gameObject }));
			
			Destroy(gameObject);
		}
	}
}