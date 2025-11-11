using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class RotateBehaviorFactory : MoveBehaviorFactory {
		public Vector3 RotateAround = Vector3.up;
		
		public override IBehavior Create(ICharacter parent) {
			return new RotateBehavior(parent, this);
		}
	}
	
	public class RotateBehavior : MoveBehavior {
		private Vector3 rotationAxis;

		public RotateBehavior(ICharacter character, RotateBehaviorFactory factory) : base(character) {
			rotationAxis = factory.RotateAround;
		}
		
		public override void Start() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			
		}

		public override void Tick(float dt) {
			parent.Transform.Rotate(rotationAxis, parent.MoveSettings.TurnSpeed * dt);
		}
		
		public override void Complete() {
			// noop
		}
	}
}