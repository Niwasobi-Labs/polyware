using System.Collections.Generic;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[CreateAssetMenu(fileName = "New Ability", menuName = "PolyWare/Ability")]
	public abstract class Ability : ScriptableObject {
		public string Name;
		[TextArea(3, 10)]
		public string Description;

		public abstract void Trigger(AbilityContextHolder ctx);
		protected abstract void ApplyEffectsTo(IAffectable target, List<IEffect> effects, AbilityContextHolder ctx);
	}
}