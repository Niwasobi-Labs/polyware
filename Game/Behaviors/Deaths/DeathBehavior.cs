using System;

namespace PolyWare.Game {
	[Serializable]
	public abstract class DeathBehaviorFactory : BehaviorFactory {
		public bool OnlyRunOnStunDeath = false;
		public bool HandleObjectDestruction = false;
	}
	
	public abstract class DeathBehavior : Behavior {
		protected readonly bool onlyRunOnStunDeath;
		public readonly bool HandleObjectDestruction;

		protected DeathBehavior(ICharacter parent, DeathBehaviorFactory factory) : base(parent, factory) {
			onlyRunOnStunDeath = factory.OnlyRunOnStunDeath;
			HandleObjectDestruction = factory.HandleObjectDestruction;
		}

		public bool OnDeath(bool wasStunned) {
			if (onlyRunOnStunDeath) {
				if (wasStunned) Start();
			}
			else {
				Start();
			}
			return HandleObjectDestruction;
		}
	}
}