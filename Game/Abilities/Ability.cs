using System;
using System.Collections.Generic;
using PolyWare.Core;

namespace PolyWare.Game {
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
			if (!Definition) {
				Log.Error("No Definition found!. How did we get here?");
				return;
			}
			var hitActions = Definition.OnSuccessActions;
			
			foreach (AbilityActionData hitAction in hitActions) {
				var targets = hitAction.Target.GetTargets(ctx);
				
				for (int j = 0; j < targets.Count; ++j) {
					if (ctx.Culprit && ctx.Culprit.TryGetComponent(out ICharacter character) && !hitAction.LuckCheck(character.Stats.GetScaledStat(CharacterStatType.Luck))) continue;
					
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
			if (!Definition) {
				Log.Error("No Definition found!. How did we get here?");
				return;
			}
			var killActions = Definition.OnKillActions;
			
			foreach (AbilityActionData killAction in killActions) {
				var targets = killAction.Target.GetTargets(ctx);
				
				for (int j = 0; j < targets.Count; ++j) {
					if (ctx.Culprit.TryGetComponent(out ICharacter character) && !killAction.LuckCheck(character.Stats.GetScaledStat(CharacterStatType.Luck))) continue;
					
					ApplyEffectsTo(targets[j], killAction.Effects, ctx);
				}
			}
		}

		private bool WasKilledByThisAbility(DamageContext damageContext, AbilityContextHolder abilityContext) {
			if (!damageContext.Ability) return false;
			
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