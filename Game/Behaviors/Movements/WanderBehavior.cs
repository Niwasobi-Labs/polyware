using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Game {
	[Serializable]
	public class WanderBehaviorFactory : MoveBehaviorFactory {
		public float WanderMinDuration = 1f;
		public float WanderMaxDuration = 2f;
		public float WanderMinDelay = 1f;
		public float WanderMaxDelay = 2f;
		
		public override IBehavior Create(ICharacter parent) {
			return new WanderBehavior(parent, this);
		}
	}
	
	public class WanderBehavior : MoveBehavior {
		private readonly float wanderMinDuration;
		private readonly float wanderMaxDuration;
		private readonly float wanderMinDelay;
		private readonly float wanderMaxDelay;
		
		private CountdownTimer wanderTimer;
		private CountdownTimer wanderDelayTimer;
		private List<Timer> timers = new List<Timer>();
		
		public WanderBehavior(ICharacter character, WanderBehaviorFactory factory) : base(character) {
			wanderMinDuration = factory.WanderMinDuration;
			wanderMaxDuration = factory.WanderMaxDuration;
			wanderMinDelay = factory.WanderMinDelay;
			wanderMaxDelay = factory.WanderMaxDelay;
			
			SetupWanderTimers();
		}

		protected override void OnStart() {
			wanderDelayTimer.Start();
		}

		private void Wander() {
			currentMoveDirection = Random.onUnitSphere * (parent.MoveSettings.Acceleration * currentSpeedStat);
			currentMoveDirection.y = 0;
			
			rb.AddForce(currentMoveDirection, ForceMode.Impulse);
			wanderTimer.Start();
		}
		
		protected override void OnTick(float dt) {
			base.OnTick(dt);
			foreach (Timer timer in timers) timer.Tick(dt);
		}
		
		protected override void OnComplete() {
			rb.linearVelocity = Vector3.zero;
			Start();
		}
		
		public override void HitWall(Vector3 hitNormal) {
			base.HitWall(hitNormal);
			rb.AddForce(currentMoveDirection, ForceMode.Impulse);
		}
		
		private void SetupWanderTimers() {
			if (wanderDelayTimer == null) {
				wanderDelayTimer = new CountdownTimer(Random.Range(wanderMinDelay, wanderMaxDelay));
				wanderDelayTimer.OnTimerComplete += Wander;
				timers.Add(wanderDelayTimer);
			}
			else {
				wanderDelayTimer.SetInitialTime(Random.Range(wanderMinDelay, wanderMaxDelay));
			}
			
			if (wanderTimer == null) {
				wanderTimer = new CountdownTimer(Random.Range(wanderMinDuration, wanderMaxDuration));
				wanderTimer.OnTimerComplete += Complete;
				timers.Add(wanderTimer);
			}
			else {
				wanderTimer.SetInitialTime(Random.Range(wanderMinDuration, wanderMaxDuration));
			}
		}
	}
}