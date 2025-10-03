using System;
using System.Collections.Generic;
using PolyWare.Debug;
using PolyWare.Effects;
using UnityEngine;

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
			
			for (int i = 0; i < Definition.OnSuccessEffects.Count; ++i) {
				var filteredTargets = Definition.OnSuccessEffects[i].Strategy.GetTargets(ctx);
				
				for (int j = 0; j < filteredTargets.Count; ++j) {
					ApplyEffectsTo(filteredTargets[j], Definition.OnSuccessEffects[i].Effects, ctx);
				}
			}
			
			isRunning = false;
			
			if (effectsHandler.IsEmpty) Dispose();
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