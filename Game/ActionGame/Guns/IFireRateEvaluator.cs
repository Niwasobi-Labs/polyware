using UnityEngine;

namespace PolyWare.Game {
	public interface IFireRateEvaluator {
		public float Evaluate(IStatsHandler stats, float baseFireRate);
	}
}