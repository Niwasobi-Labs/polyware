using System;
using System.Collections.Generic;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Game {
	public enum WallCollisionMode {
		Reflect,
		Reverse,
		None
	}
	
	[Serializable]
	public class MoveInDirectionBehaviorFactory : MoveBehaviorFactory {
		public WallCollisionMode WallCollisionMode;
		
		[Title("Rotation")]
		public bool SetForwardOnRotate;
		public bool RandomStartingDirection;
		[ShowIf("RandomStartingDirection")] public Vector3 RotateAround = Vector3.up;
		
		[Title("Wander")]
		public bool Wander = false;
		[ShowIf("Wander")] public float WanderMinDuration = 1f;
		[ShowIf("Wander")] public float WanderMaxDuration = 2f;
		[ShowIf("Wander")] public float WanderMinDelay = 1f;
		[ShowIf("Wander")] public float WanderMaxDelay = 2f;
		
		
		public override IBehavior Create(ICharacter parent) {
			return new MoveInDirectionBehavior(parent, this);
		}
	}
	
	public class MoveInDirectionBehavior : MoveBehavior {
		private readonly bool randomStartingDirection;
		private readonly bool setForwardOnRotate;
		private readonly Vector3 rotateAround;
		private readonly WallCollisionMode wallCollisionMode;
		private bool wander;
		private readonly float wanderMinDuration;
		private readonly float wanderMaxDuration;
		private readonly float wanderMinDelay;
		private readonly float wanderMaxDelay;

		private Vector3 currentMoveDirection;
		private float speed;
		private CountdownTimer wanderTimer;
		private CountdownTimer wanderDelayTimer;
		private List<Timer> timers = new List<Timer>();
		
		public MoveInDirectionBehavior(ICharacter character, MoveInDirectionBehaviorFactory factory) : base(character) {
			wallCollisionMode = factory.WallCollisionMode;
			
			setForwardOnRotate = factory.SetForwardOnRotate;
			randomStartingDirection = factory.RandomStartingDirection;
			rotateAround = factory.RotateAround;
			
			wander = factory.Wander;
			wanderMinDuration = factory.WanderMinDuration;
			wanderMaxDuration = factory.WanderMaxDuration;
			wanderMinDelay = factory.WanderMinDelay;
			wanderMaxDelay = factory.WanderMaxDelay;
			
			if (wander) SetupWanderTimers();
		}
		
		public override void Start() {
			if (randomStartingDirection) {
				FindNewRandomDirection();
			}
			else {
				currentMoveDirection = parent.Transform.forward;
			}

			if (setForwardOnRotate) {
				parent.Transform.forward = currentMoveDirection;
			}
			
			if (wander) StartWandering();
		}
		
		public override void HitWall(Vector3 hitNormal) {
			
			switch (wallCollisionMode) {
				case WallCollisionMode.Reflect:
					currentMoveDirection = Vector3.Reflect(currentMoveDirection, hitNormal);
					rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, hitNormal);
					break;
				case WallCollisionMode.Reverse:
					currentMoveDirection = -currentMoveDirection;
					rb.linearVelocity = Vector3.Reflect(-rb.linearVelocity, hitNormal);
					break;
				case WallCollisionMode.None:
					break;
			}

			currentMoveDirection.Normalize();
			
			if (setForwardOnRotate) {
				parent.Transform.forward = currentMoveDirection;
			}
		}

		public override void Tick(float dt) {
			foreach (Timer timer in timers) timer.Tick(dt);

			if (!wander || wanderTimer.IsRunning) {
				Move();
			}
		}

		private void FindNewRandomDirection() {
			currentMoveDirection = Random.onUnitSphere;
			currentMoveDirection.y = 0; // todo: 2D games only (fix for 3D)
		}
		
		private void StartWandering() {
			FindNewRandomDirection();
			wanderTimer.Start();
		}

		private void SetupWanderTimers() {
			if (wanderDelayTimer == null) {
				wanderDelayTimer = new CountdownTimer(Random.Range(wanderMinDelay, wanderMaxDelay));
				wanderDelayTimer.OnTimerComplete += StartWandering;
				timers.Add(wanderDelayTimer);
			}
			else {
				wanderDelayTimer.SetInitialTime(Random.Range(wanderMinDelay, wanderMaxDelay));
			}
			
			if (wanderTimer == null) {
				wanderTimer = new CountdownTimer(Random.Range(wanderMinDuration, wanderMaxDuration));
				wanderTimer.OnTimerComplete += wanderDelayTimer.Start;
				timers.Add(wanderTimer);
			}
			else {
				wanderTimer.SetInitialTime(Random.Range(wanderMinDuration, wanderMaxDuration));
			}
		}
		
		private void Move() {
			float speedStat = parent.Stats.GetModifiedStat(StatType.Speed);
			float maxSpeed = parent.MoveSettings.MaxMoveSpeed * parent.Stats.GetModifiedStat(StatType.Speed);
			rb.maxLinearVelocity = maxSpeed;
			
			currentMoveDirection.Normalize();
			currentMoveDirection *= parent.MoveSettings.MaxMoveSpeed * speedStat;
			currentMoveDirection.y = 0;
			
			//rb.linearVelocity = currentMovement;
			rb.AddForce(currentMoveDirection, ForceMode.Force);
		}
		
		public override void Complete() {
			// noop
		}
	}
}