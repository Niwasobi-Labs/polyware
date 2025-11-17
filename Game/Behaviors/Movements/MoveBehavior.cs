using UnityEngine;

namespace PolyWare.Game {
	public abstract class MoveBehaviorFactory : IBehaviorFactory {
		public WallCollisionMode WallCollisionMode;
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class MoveBehavior : Behavior {
		protected readonly Rigidbody rb;
		protected float currentSpeedStat;
		protected Vector3 currentMoveDirection;
		protected WallCollisionMode wallCollisionMode;
		
		protected MoveBehavior(ICharacter character) : base(character) {
			rb = parent.GameObject.GetComponent<Rigidbody>();
			UpdateMoveSettings();
		}

		protected override void OnTick(float dt) {
			UpdateMoveSettings();
		}

		public virtual void HitWall(Vector3 hitNormal) {
			switch (wallCollisionMode) {
				case WallCollisionMode.Reflect:
					currentMoveDirection = Vector3.Reflect(currentMoveDirection, hitNormal);
					break;
				case WallCollisionMode.Reverse:
					currentMoveDirection = -currentMoveDirection;
					break;
				case WallCollisionMode.None:
					break;
			}

			currentMoveDirection.Normalize();
		}

		protected void UpdateMoveSettings() {
			currentSpeedStat = parent.Stats.GetModifiedStat(StatType.Speed);
			rb.maxLinearVelocity = parent.MoveSettings.MaxMoveSpeed * currentSpeedStat;
		}
	}
}