using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[System.Serializable]
	public class LookAtPlayerBehaviorFactory : MoveBehaviorFactory {
		public bool Instant = true;
		
		public override IBehavior Create(ICharacter parent) {
			return new LookAtPlayerBehavior(parent, this);
		}
	} 
	
	public class LookAtPlayerBehavior : MoveBehavior {
		private readonly bool instant;

		private Transform player;
		
		public LookAtPlayerBehavior(ICharacter character, LookAtPlayerBehaviorFactory factory) : base(character) {
			instant = factory.Instant;
		}
			
		public override void Start() {
			FindPlayer();
		}

		private void FindPlayer() {
			player = ServiceLocator.Global.Get<IGameService>().GetLocalPlayerCharacter.Transform;
		}
		
		public override void Tick(float dt) {
			if (!player) FindPlayer();
			
			if (instant) parent.Transform.LookAt(player, parent.Transform.up);
		}
		
		public override void Complete() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			// noop
		}
	}
}