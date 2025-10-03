using System;
using System.Collections.Generic;
using PolyWare.Abilities;
using UnityEngine;

namespace PolyWare.Effects {
	[Serializable]
	public class EffectData {
		[SerializeReference] public IEffectStrategy Strategy;
		[SerializeReference] public List<IEffect> Effects;
	}
}