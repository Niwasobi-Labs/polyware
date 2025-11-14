using UnityEngine;

namespace PolyWare.Game {
	public abstract class MoveBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class MoveBehavior : Behavior {
		protected readonly Rigidbody rb;
		
		protected MoveBehavior(ICharacter character) : base(character) {
			rb = parent.GameObject.GetComponent<Rigidbody>();
		}
		
		public abstract void HitWall(Vector3 hitNormal);
	}
}