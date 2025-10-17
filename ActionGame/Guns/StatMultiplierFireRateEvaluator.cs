using PolyWare.Characters;
using PolyWare.Stats;
using UnityEngine;

namespace PolyWare.ActionGame.Guns {
	public class StatMultiplierFireRateEvaluator : IFireRateEvaluator {
		public StatType Stat;

		public float Evaluate(GameObject culprit, float baseFireRate) {
			if (culprit && culprit.TryGetComponent(out ICharacter character)) {
				baseFireRate /= character.Stats.GetModifiedStat(Stat);
			}
			
			return baseFireRate;
		}
	}
}