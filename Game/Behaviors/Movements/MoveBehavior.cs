using UnityEngine;

namespace PolyWare.Game {
	public abstract class MoveBehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class MoveBehavior : IBehavior {
		protected readonly ICharacter parent;
		protected readonly Rigidbody rb;
		
		protected MoveBehavior(ICharacter character) {
			parent = character;
			rb = parent.GameObject.GetComponent<Rigidbody>();
		}
		
		public abstract void Start();
		public abstract void HitWall(Vector3 hitNormal);
		public abstract void Tick(float dt);
		public abstract void Complete();
	}
}