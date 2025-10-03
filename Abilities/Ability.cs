using System;
using System.Collections.Generic;
using PolyWare.Combat;
using PolyWare.Effects;

namespace PolyWare.Abilities {
	public class Ability : IDisposable {
		public AbilityDefinition Definition;
		
		private readonly IEffectsHandler effectsHandler;
		private bool isRunning = false;
		
		public Ability(AbilityDefinition definition) {
			Definition = definition;
			
			effectsHandler = new EffectsHandler();
			effectsHandler.OnEmpty += Dispose;
		}

		public void Trigger(AbilityContextHolder ctx) {
			isRunning = true;

			TriggerOnHitEffects(ctx);
			
			isRunning = false;
			
			if (effectsHandler.IsEmpty) Dispose();
		}

		protected void TriggerOnHitEffects(AbilityContextHolder ctx) {
			for (int i = 0; i < Definition.OnHit.Count; ++i) {
				var filteredTargets = Definition.OnHit[i].Strategy.GetTargets(ctx);
				
				for (int j = 0; j < filteredTargets.Count; ++j) {
					
					if (filteredTargets[j] is IDamageable damageable) {
						damageable.OnDeath += (damage) => {
							if (WasKilledByThisAbility(damage, ctx)) TriggerOnKillEffects(ctx);
						};
					}
					
					ApplyEffectsTo(filteredTargets[j], Definition.OnHit[i].Effects, ctx);
				}
			}
		}

		protected void TriggerOnKillEffects(AbilityContextHolder ctx) {
			for (int i = 0; i < Definition.OnKill.Count; ++i) {
				var filteredTargets = Definition.OnKill[i].Strategy.GetTargets(ctx);
				
				for (int j = 0; j < filteredTargets.Count; ++j) {
					ApplyEffectsTo(filteredTargets[j], Definition.OnKill[i].Effects, ctx);
				}
			}
		}

		private bool WasKilledByThisAbility(DamageContext damageContext, AbilityContextHolder abilityContext) {
			return damageContext.Culprit == abilityContext.Culprit && damageContext.Ability == Definition;
		}
		
		protected void ApplyEffectsTo(IAffectable target, List<IEffect> effects, AbilityContextHolder ctx) {
			for (int i = 0; i < effects.Count; ++i) {
				effectsHandler.Add(effects[i]);
				target.Affect(effects[i], ctx);
			}
		}
		
		public void Dispose() {
			if (isRunning) return;
			effectsHandler.OnEmpty -= Dispose;
			Definition = null;
			effectsHandler.RemoveAll();
		}
	}
}