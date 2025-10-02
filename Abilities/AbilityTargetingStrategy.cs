using System;
using PolyWare.Core;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	public enum TargetingStrategyType {
		Simple,
		Projectile,
		Self,
	}
	
	public abstract class AbilityTargetingStrategy {
		public static AbilityTargetingStrategy Create(TargetingStrategyType type) {
			return type switch {
				TargetingStrategyType.Simple => new SimpleAbilityTargeting(),
				TargetingStrategyType.Projectile => new ProjectileAbilityTargeting(),
				TargetingStrategyType.Self => new SelfAbilityTargeting(),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}

		public abstract void Start(AbilityContextHolder contextHolder);
		public virtual void Update() { }
		public virtual void Cancel() { }
	}
}