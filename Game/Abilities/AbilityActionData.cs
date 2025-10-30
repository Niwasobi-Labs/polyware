using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class AbilityActionData {
		[SerializeReference] public IActionTargetStrategy Target;
		[SerializeReference] public List<IEffectFactory> Effects = new ();
	}
}