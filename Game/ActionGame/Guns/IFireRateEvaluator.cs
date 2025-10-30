using UnityEngine;

namespace PolyWare.Game {
	public interface IFireRateEvaluator {
		public float Evaluate(GameObject culprit, float baseFireRate);
	}
}