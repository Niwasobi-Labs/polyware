using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Game {
	[Serializable]
	public class RotateBehaviorFactory : MoveBehaviorFactory {
		public bool RandomRotation = true;
		[HideIf("RandomRotation")] public Vector3 RotateAround = Vector3.up;
		
		public override IBehavior Create(ICharacter parent) {
			return new RotateBehavior(parent, this);
		}
	}
	
	public class RotateBehavior : MoveBehavior {
		private Vector3 rotationAxis;

		public RotateBehavior(ICharacter character, RotateBehaviorFactory factory) : base(character) {
			rotationAxis = factory.RandomRotation ? Random.rotation.eulerAngles : factory.RotateAround;
		}
		
		protected override void OnStart() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			
		}

		protected override void OnTick(float dt) {
			parent.Transform.Rotate(rotationAxis, parent.MoveSettings.TurnSpeed * dt);
		}
		
		protected override void OnComplete() {
			// noop
		}
	}
}