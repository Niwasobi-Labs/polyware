using System;
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
		public bool RandomStartingDirection;
		public bool AllowRotation;
		[ShowIf("RandomStartingDirection")] public Vector3 RotateAround = Vector3.up;
		
		public WallCollisionMode WallCollisionMode;
		
		public override IBehavior Create(ICharacter parent) {
			return new MoveInDirectionBehavior(parent, this);
		}
	}
	
	public class MoveInDirectionBehavior : MoveBehavior {
		private float speed;
		private readonly bool randomStartingDirection;
		private readonly bool allowRotation;
		private readonly Vector3 rotateAround;
		private WallCollisionMode wallCollisionMode;

		private Vector3 fixedDirection;
		
		public MoveInDirectionBehavior(ICharacter character, MoveInDirectionBehaviorFactory factory) : base(character) {
			randomStartingDirection = factory.RandomStartingDirection;
			rotateAround = factory.RotateAround;
			allowRotation = factory.AllowRotation;
		}
		
		public override void Start() {
			if (randomStartingDirection) {
				parent.Transform.rotation = Quaternion.Euler(rotateAround * Random.Range(0, 360));
			}

			if (!allowRotation) {
				fixedDirection = parent.Transform.forward;
			}
		}

		public override void HitWall(Vector3 hitNormal) {

			Vector3 currentDirection = allowRotation ? parent.Transform.forward : fixedDirection;
			
			switch (wallCollisionMode) {
				case WallCollisionMode.Reflect:
					currentDirection = Vector3.Reflect(currentDirection, hitNormal);
					break;
				case WallCollisionMode.Reverse:
					currentDirection = -currentDirection;
					break;
				case WallCollisionMode.None:
					break;
			}

			if (allowRotation) {
				parent.Transform.forward = currentDirection;
			}
			else {
				fixedDirection = currentDirection;
			}
		}

		public override void Tick(float dt) {
			float speedStat = parent.Stats.GetModifiedStat(StatType.Speed);
			float maxSpeed = parent.MoveSettings.MaxMoveSpeed * parent.Stats.GetModifiedStat(StatType.Speed);
			rb.maxLinearVelocity = maxSpeed;
			
			Vector3 currentMovement = (allowRotation ? parent.Transform.forward : fixedDirection).normalized;
			currentMovement *= parent.MoveSettings.MaxMoveSpeed * speedStat;
			currentMovement.y = 0;
			
			//rb.linearVelocity = currentMovement;
			rb.AddForce(currentMovement, ForceMode.Force);
		}
		
		public override void Complete() {
			// noop
		}
	}
}