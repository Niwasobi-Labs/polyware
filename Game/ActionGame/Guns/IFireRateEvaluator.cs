using UnityEngine;

namespace PolyWare.ActionGame.Guns {
	public interface IFireRateEvaluator {
		public float Evaluate(GameObject culprit, float baseFireRate);
	}
}