using System.Collections.Generic;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[CreateAssetMenu(fileName = "New Ability", menuName = "PolyWare/Ability")]
	public class SimpleAbility : Ability {
		public List<EffectData> OnSuccessEffects = new List<EffectData>();
		
		public override void Trigger(AbilityContextHolder ctx) {
			for (int i = 0; i < OnSuccessEffects.Count; ++i) {
				var filteredTargets = OnSuccessEffects[i].Strategy.GetTargets(ctx);
				
				for (int j = 0; j < filteredTargets.Count; ++j) {
					ApplyEffectsTo(filteredTargets[j], OnSuccessEffects[i].Effects, ctx);
				}
			}
		}
		
		protected override void ApplyEffectsTo(IAffectable target, List<IEffect> effects, AbilityContextHolder ctx) {
			for (int i = 0; i < effects.Count; ++i) {
				target.Affect(effects[i], ctx);
			}
		}
	}
}