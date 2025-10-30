using UnityEngine;

namespace PolyWare.Game {
	public class DefaultFireRateEvaluator : IFireRateEvaluator {
		public float Evaluate(GameObject culprit, float baseFireRate) => baseFireRate;
	}
}