using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class MoveForwardBehaviorFactory : MoveBehaviorFactory {
		public override IBehavior Create(ICharacter parent) {
			return new MoveForwardBehavior(parent, this);
		}
	}
	
	public class MoveForwardBehavior : MoveBehavior {
		
		public MoveForwardBehavior(ICharacter character, MoveForwardBehaviorFactory factory) : base(character) {

		}
		
		public override void Start() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			
		}

		public override void Tick(float dt) {
			float speedStat = parent.Stats.GetModifiedStat(StatType.Speed);
			float maxSpeed = parent.MoveSettings.MaxMoveSpeed * parent.Stats.GetModifiedStat(StatType.Speed);
			rb.maxLinearVelocity = maxSpeed;
			
			//rb.linearVelocity = currentMovement;
			rb.AddForce(rb.transform.forward * parent.MoveSettings.MaxMoveSpeed * speedStat, ForceMode.Force);
		}
		
		public override void Complete() {
			// noop
		}
	}
}