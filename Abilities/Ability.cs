using System.Collections.Generic;
using PolyWare.Core;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[CreateAssetMenu(menuName = "PolyWare/Ability",  fileName = "New CharacterAbility")]
	public class Ability : ScriptableObject {
		public string Name;
		public string Description;
		public TargetingStrategyType TargetStrategy;
		[SerializeReference] protected List<IEffect> effects;

		public void Trigger(AbilityContextHolder ctx) {
			var strategy = AbilityTargetingStrategy.Create(TargetStrategy);
			strategy.Start(ctx);
		}

		public void Execute(IAffectable target, ContextHolder ctx) {
			for (int i = 0; i < effects.Count; i++) {
				target.Affect(effects[i], ctx);
			}
		}
	}
}