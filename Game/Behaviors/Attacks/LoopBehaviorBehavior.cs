using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class LoopBehaviorBehaviorFactory : AttackBehaviorFactory {
		public int UseCount = -1;
		public float Cooldown = 1;
		public float TelegraphTime = 1f;
		[SerializeReference] public IBehaviorFactory Behavior;
		
		public override IBehavior Create(ICharacter character) {
			return new LoopBehaviorBehavior(character, this);
		}
	}
	
	public class LoopBehaviorBehavior : AttackBehavior {
		private readonly CountdownTimer cooldownTimer;
		private readonly int useCount;
		
		private readonly ITelegrapher telegrapher;
		private readonly CountdownTimer telegraphTimer;
		
		private int uses;
		private readonly IBehavior behavior;
		
		public LoopBehaviorBehavior(ICharacter character, LoopBehaviorBehaviorFactory factory) : base(character) {
			cooldownTimer = new CountdownTimer(factory.Cooldown);
			cooldownTimer.OnTimerComplete += CastAbility;
			useCount = factory.UseCount;
			behavior = factory.Behavior.Create(character);
			
			telegrapher = character.GameObject.GetComponent<ITelegrapher>();
			telegraphTimer = new CountdownTimer(factory.TelegraphTime);
			telegraphTimer.OnTimerComplete += StartBehavior;
		}

		protected override void OnStart() {
			uses = 0;
			cooldownTimer.Start();
		}

		private void CastAbility() {
			if (telegraphTimer == null) {
				StartBehavior();
				return;
			}
			
			telegraphTimer.Start();
			telegrapher?.StartTelegraphing(telegraphTimer.InitialTime);
		}
		
		private void StartBehavior() {
			telegrapher?.StopTelegraphing();
			uses++;
			behavior.Start();
			
			if (uses < useCount || useCount == -1) cooldownTimer.Restart();
			else Complete();
		}
		
		protected override void OnTick(float dt) {
			cooldownTimer.Tick(dt);
			telegraphTimer?.Tick(dt);
			if (behavior.IsRunning) behavior.Tick(dt);
		}

		public override void OnPlayerHit(ICharacter player) {
				
		}

		protected override void OnComplete() {
			// no-op
		}
	}
}