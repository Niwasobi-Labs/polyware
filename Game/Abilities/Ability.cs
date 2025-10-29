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

			TriggerOnHitActions(ctx);
			
			isRunning = false;
			
			if (effectsHandler.IsEmpty) Dispose();
		}

		protected void TriggerOnHitActions(AbilityContextHolder ctx) {
			var hitActions = Definition.OnSuccessActions;
			
			foreach (AbilityActionData hitAction in hitActions) {
				var targets = hitAction.Target.GetTargets(ctx);
				
				for (int j = 0; j < targets.Count; ++j) {
					SubscribeToTargetDeath(targets[j], ctx);
					ApplyEffectsTo(targets[j], hitAction.Effects, ctx);
				}
			}
		}

		private void SubscribeToTargetDeath(IAffectable target, AbilityContextHolder ctx) {
			if (target is IDamageable damageable) {
				damageable.OnDeath += (damage) => {
					if (WasKilledByThisAbility(damage, ctx)) TriggerOnKillActions(ctx);
				};
			}
		}
		
		protected void TriggerOnKillActions(AbilityContextHolder ctx) {
			var killActions = Definition.OnKillActions;
			
			foreach (AbilityActionData killAction in killActions) {
				var targets = killAction.Target.GetTargets(ctx);
				
				for (int j = 0; j < targets.Count; ++j) {
					ApplyEffectsTo(targets[j], killAction.Effects, ctx);
				}
			}
		}

		private bool WasKilledByThisAbility(DamageContext damageContext, AbilityContextHolder abilityContext) {
			return damageContext.Culprit == abilityContext.Culprit && damageContext.Ability == Definition;
		}
		
		protected void ApplyEffectsTo(IAffectable target, List<IEffectFactory> effects, AbilityContextHolder ctx) {
			for (int i = 0; i < effects.Count; ++i) {
				var effect = effects[i].Create();
				effectsHandler.Add(effect);
				target.Affect(effect, ctx);
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