using System;
using System.Collections.Generic;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Abilities {
	[Serializable]
	public class AbilityActionData {
		[SerializeReference] public IActionTargetStrategy Target;
		[SerializeReference] public List<IEffectFactory> Effects = new ();
	}
}