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
		[Title("Rotation")]
		public bool SetForwardOnRotate;
		public bool RandomStartingDirection;
		[ShowIf("RandomStartingDirection")] public Vector3 RotateAround = Vector3.up;
		[HideIf("RandomStartingDirection")] public Vector3 FixedWorldMove = Vector3.forward;
		
		public override IBehavior Create(ICharacter parent) {
			return new MoveInDirectionBehavior(parent, this);
		}
	}
	
	public class MoveInDirectionBehavior : MoveBehavior {
		private readonly bool randomStartingDirection;
		private readonly bool setForwardOnRotate;
		private readonly Vector3 fixedWorldMove;
		
		public MoveInDirectionBehavior(ICharacter character, MoveInDirectionBehaviorFactory factory) : base(character, factory) {
			wallCollisionMode = factory.WallCollisionMode;
			
			setForwardOnRotate = factory.SetForwardOnRotate;
			randomStartingDirection = factory.RandomStartingDirection;
			
			fixedWorldMove = factory.FixedWorldMove;
		}

		protected override void OnStart() {
			if (randomStartingDirection) {
				FindNewRandomDirection();
			}
			else {
				currentMoveDirection = fixedWorldMove;
			}

			if (setForwardOnRotate) {
				parent.Transform.forward = currentMoveDirection;
			}

			Move();
		}
		
		private void FindNewRandomDirection() {
			currentMoveDirection = Random.onUnitSphere;
			currentMoveDirection.y = 0; // todo: 2D games only (fix for 3D)
			Move();
		}
		
		public override void HitWall(Vector3 hitNormal) {
			base.HitWall(hitNormal);
			if (setForwardOnRotate) {
				parent.Transform.forward = currentMoveDirection;
			}

			Move();
		}
		
		private void Move() {
			currentMoveDirection.Normalize();
			currentMoveDirection *= parent.MoveSettings.Acceleration * currentSpeedStat;
			currentMoveDirection.y = 0;
			
			rb.AddForce(currentMoveDirection, ForceMode.Impulse);
		}
		
		protected override void OnComplete() {
			// noop
		}
	}
}