using System.Collections.Generic;
using PolyWare.Combat;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[CreateAssetMenu(fileName = "New Ability", menuName = "PolyWare/Abilities/Ability")]
	public class Ability : ScriptableObject {
		public string Name;
		public string Description;
		public TargetingStrategyType TargetStrategy;
		[SerializeReference] protected List<IEffect<IDamageable>> effects;

		public void Trigger(AbilityContext context) {
			var strategy = TargetingStrategy.Create(TargetStrategy);
			strategy.Start(context);
		}

		public void Execute(IDamageable target, AbilityContext context) {
			for (int i = 0; i < effects.Count; i++) {
				target.ApplyEffect(effects[i], context);
			}
		}
	}
}