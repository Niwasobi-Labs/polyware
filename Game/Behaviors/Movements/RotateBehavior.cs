using System;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Game {
	[Serializable]
	public class RotateBehaviorFactory : MoveBehaviorFactory {
		public bool RandomRotation = true;
		public int ChangeDirectionsAfter = -1;
		[HideIf("RandomRotation")] public Vector3 RotateAround = Vector3.up;
		
		public override IBehavior Create(ICharacter parent) {
			return new RotateBehavior(parent, this);
		}
	}
	
	public class RotateBehavior : MoveBehavior {
		private Vector3 rotationAxis;

		private CountdownTimer changeDirectionTimer;
		private float directionModifier;
		
		public RotateBehavior(ICharacter character, RotateBehaviorFactory factory) : base(character, factory) {
			rotationAxis = factory.RandomRotation ? Random.rotation.eulerAngles : factory.RotateAround;
			directionModifier = 1;
			
			if (factory.ChangeDirectionsAfter > 0) {
				changeDirectionTimer = new CountdownTimer(factory.ChangeDirectionsAfter);
				changeDirectionTimer.OnTimerComplete += () => {
					directionModifier *= -1;
					changeDirectionTimer.Restart();
				};
				
			}
		}
		
		protected override void OnStart() {
			changeDirectionTimer?.Start();
		}

		public override void HitWall(Vector3 hitNormal) {
			
		}

		protected override void OnTick(float dt) {
			parent.Transform.Rotate(rotationAxis, (parent.MoveSettings.TurnSpeed * directionModifier) * dt);
			changeDirectionTimer?.Tick(dt);
		}
		
		protected override void OnComplete() {
			// noop
		}
	}
}