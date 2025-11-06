using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[System.Serializable]
	public class LookAtPlayerBehaviorFactory : MoveBehaviorFactory {
		
		public override IBehavior Create(ICharacter parent) {
			return new LookAtPlayerBehavior(parent, this);
		}
	} 
	
	public class LookAtPlayerBehavior : MoveBehavior {

		private Transform player;
		
		public LookAtPlayerBehavior(ICharacter character, LookAtPlayerBehaviorFactory factory) : base(character) {
			
		}
			
		public override void Start() {
			FindPlayer();
		}

		private void FindPlayer() {
			if (ServiceLocator.Global.TryGet(out IGameService gameService)) {
				player = gameService.GetLocalPlayerCharacter?.Transform;
			}
		}
		
		public override void Tick(float dt) {
			if (!player) FindPlayer();
			if (!player) return;

			parent.Transform.LookAt(player, parent.Transform.up);
		}
		
		public override void Complete() {
			// noop
		}

		public override void HitWall(Vector3 hitNormal) {
			// noop
		}
	}
}