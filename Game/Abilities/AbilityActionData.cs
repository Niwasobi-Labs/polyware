using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class AbilityActionData {
		[SerializeReference] public IActionTargetStrategy Target;
		[SerializeReference] public List<IEffectFactory> Effects = new ();
		[SerializeField] [Range(0, 1)] private float chance = 1f;
		[SerializeField] private bool affectedByLuck = true;

		public bool LuckCheck(float luckStat) {
			float roll = UnityEngine.Random.value;
			return affectedByLuck ? roll >= 1 - chance * luckStat : roll >= 1 - chance;  
		}
	}
}