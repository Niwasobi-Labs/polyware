using System.Collections.Generic;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[CreateAssetMenu(fileName = "New AbilityDefinition", menuName = "PolyWare/Ability")]
	public class AbilityDefinition : ScriptableObject {
		public string Name;
		[TextArea(3, 10)]
		public string Description;
		public List<EffectData> OnSuccessEffects;

		public Ability CreateInstance() {
			return new Ability(this);
		}
	}
}