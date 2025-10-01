using System;

namespace PolyWare.Abilities {
	public enum TargetingStrategyType {
		Projectile,
		Self
	}
	
	public abstract class TargetingStrategy {
		public static TargetingStrategy Create(TargetingStrategyType type) {
			return type switch {
				TargetingStrategyType.Projectile => new ProjectileTargeting(),
				TargetingStrategyType.Self => new SelfTargeting(),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
		
		public abstract void Start(AbilityContext context);
		public virtual void Update() { }
		public virtual void Cancel() { }
	}
}