using UnityEngine;

namespace PolyWare.ActionGame.Guns {
	public class DefaultFireRateEvaluator : IFireRateEvaluator {
		public float Evaluate(GameObject culprit, float baseFireRate) => baseFireRate;
	}
}