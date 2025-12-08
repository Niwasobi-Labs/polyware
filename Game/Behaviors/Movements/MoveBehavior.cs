using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public abstract class MoveBehaviorFactory : IBehaviorFactory {
		public WallCollisionMode WallCollisionMode;
		public int WallHitsLimit = -1;
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class MoveBehavior : Behavior {
		protected readonly Rigidbody rb;
		private readonly int wallHitLimit;
		
		protected float currentSpeedStat;
		protected Vector3 currentMoveDirection;
		protected WallCollisionMode wallCollisionMode;
		
		protected int wallHits;
		
		protected MoveBehavior(ICharacter character, MoveBehaviorFactory factory) : base(character) {
			rb = parent.GameObject.GetComponent<Rigidbody>();
			wallHitLimit = factory.WallHitsLimit;
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
			
			wallHits++;
			if (wallHitLimit >= 0 && wallHits >= wallHitLimit) {
				parent.Kill(new DamageContext(Element.Kinetic, 0, null, null));
			}
		}

		protected void UpdateMoveSettings() {
			currentSpeedStat = parent.Stats.GetModifiedStat(StatType.Agility);
			rb.maxLinearVelocity = parent.MoveSettings.MaxMoveSpeed * currentSpeedStat;
		}
	}
}