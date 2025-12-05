using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class AlwaysFaceInDirectionBehaviorFactory : MoveBehaviorFactory {
		public Vector3 WorldLook = Vector3.forward;
		
		public override IBehavior Create(ICharacter parent) {
			return new AlwaysFaceInDirectionBehavior(parent, this);
		}
	}

	public class AlwaysFaceInDirectionBehavior : MoveBehavior {
		private readonly Vector3 fixedForward;
		
		public AlwaysFaceInDirectionBehavior(ICharacter character, AlwaysFaceInDirectionBehaviorFactory factory) : base(character, factory) {
			fixedForward = factory.WorldLook;	
		}
		
		protected override void OnStart() {
			rb.transform.forward = fixedForward;
		}
		
		protected override void OnComplete() {
			// no-op
		}
	}
}