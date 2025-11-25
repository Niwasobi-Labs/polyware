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
		
		public LookAtPlayerBehavior(ICharacter character, LookAtPlayerBehaviorFactory factory) : base(character, factory) {
			
		}
			
		protected override void OnStart() {
			FindPlayer();
		}

		private void FindPlayer() {
			if (ServiceLocator.Global.TryGet(out IGameService gameService)) {
				player = gameService.GetLocalPlayerCharacter?.Transform;
			}
		}
		
		protected override void OnTick(float dt) {
			if (!player) FindPlayer();
			if (!player) return;

			Vector3 direction = (player.position - parent.Transform.position).normalized;
			if (direction.sqrMagnitude < 0.0001f) return;

			Quaternion targetRotation = Quaternion.LookRotation(direction, parent.Transform.up);
			parent.Transform.rotation = Quaternion.Slerp(
				parent.Transform.rotation,
				targetRotation,
				dt * parent.MoveSettings.TurnSpeed
			);
		}

		public override void HitWall(Vector3 hitNormal) {
			// noop
		}

		protected override void OnComplete() {
			
		}
	}
}