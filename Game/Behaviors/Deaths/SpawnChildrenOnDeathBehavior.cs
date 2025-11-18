using System;

namespace PolyWare.Game {
	[Serializable]
	public class SpawnChildrenOnDeathBehaviorFactory : DeathBehaviorFactory {
		public SpawnChildrenContext SpawnSettings;
		
		public override IBehavior Create(ICharacter parent) {
			return new SpawnChildrenOnDeathBehavior(parent, this);
		}
	}
	
	public class SpawnChildrenOnDeathBehavior : DeathBehavior {
		private readonly SpawnChildrenContext spawnSettings;

		public SpawnChildrenOnDeathBehavior(ICharacter parent, SpawnChildrenOnDeathBehaviorFactory factory) : base(parent, factory) {
			spawnSettings = factory.SpawnSettings;
		}
		
		protected override void OnStart() {
			Complete();
		}
		
		protected override void OnTick(float dt) {
			// noop
		}
		
		protected override void OnComplete() {
			SpawnChildrenHelper.Spawn(parent.GameObject, spawnSettings, null);
		}
	}
}