using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class MoveForwardBehaviorFactory : MoveBehaviorFactory {
		public override IBehavior Create(ICharacter parent) {
			return new MoveForwardBehavior(parent, this);
		}
	}
	
	public class MoveForwardBehavior : MoveBehavior {
		
		public MoveForwardBehavior(ICharacter character, MoveForwardBehaviorFactory factory) : base(character, factory) {

		}
		
		protected override void OnStart() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			
		}

		protected override void OnTick(float dt) {
			base.OnTick(dt);
			
			rb.linearVelocity = rb.transform.forward * (parent.MoveSettings.Acceleration * currentSpeedStat);
		}
		
		protected override void OnComplete() {
			// noop
		}
	}
}