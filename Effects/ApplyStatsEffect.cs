using System;
using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Characters;
using PolyWare.Combat;
using PolyWare.Core;
using PolyWare.Stats;

namespace PolyWare.Effects {

	public class ApplyStatsEffectContext : IContext {
		public List<StatModifier> Modifiers;
		
		public ApplyStatsEffectContext(List<StatModifier> modifiers) {
			Modifiers = modifiers;
		}
	}
	
	public class ApplyStatsEffect : IEffect<IDamageable> {
		public event Action<IEffect<IDamageable>> OnCompleted;
		
		public void Apply(IDamageable target, AbilityContext effectContext) {
			if (!target.GameObject.TryGetComponent(out ICharacter character)) return;
			
			var statContext = effectContext.Get<ApplyStatsEffectContext>();
			
			for (int i = 0; i < statContext.Modifiers.Count; ++i) {
				character.Stats.AddModifier(statContext.Modifiers[i]);
			}
			
			OnCompleted?.Invoke(this);
		}
		
		public void Update(float deltaTime) {
			// no-op
		}
		
		public void Cancel() {
			// no-op
		}
	}
}