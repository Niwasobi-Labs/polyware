using System;
using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Characters;
using PolyWare.Debug;
using PolyWare.Core.Entities;
using PolyWare.Effects;
using PolyWare.Interactions;
using PolyWare.Items;
using PolyWare.Stats;
using PolyWare.Timers;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	public class PowerUp : Entity<PowerUpData>, IPickupable {
		protected ICharacter character;

		// todo: remove the user from Pickup and handle power-ups through this auto-prop
		public bool AutoPickup => true;
		public override PowerUpData Data { get; protected set; }
		
		private CountdownTimer lifeTimer;
		private List<StatModifier> modifiers = new List<StatModifier>();

		protected override void OnInitialize() {
			lifeTimer = new CountdownTimer(Data.Definition.LifeTime) {
				OnTimerComplete = () => Destroy(gameObject)
			};

			for (int i = 0; i < Data.Definition.StatData.Count; i++) {
				StatModiferData statCtx = Data.Definition.StatData[i];
				StatModifier modifier = statCtx.Type switch {
					StatModiferData.OperatorType.Add => new BasicStatModifier(statCtx.StatType, statCtx.Duration, v => v + statCtx.Value),
					StatModiferData.OperatorType.Multiply => new BasicStatModifier(statCtx.StatType, statCtx.Duration, v => v * statCtx.Value),
					_ => throw new ArgumentOutOfRangeException()
				};
				modifiers.Add(modifier);
			}
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
			var context = new AbilityContext(
				Data.Definition.OnPickupAbility,
				character.FactionMember.FactionID,
				gameObject,
				new List<GameObject> { character.Transform.gameObject },
				transform.position,
				transform.rotation);
			
			context.Add(new ApplyStatsEffectContext(modifiers));

			Data.Definition.OnPickupAbility.Trigger(context);
		}
	}
}