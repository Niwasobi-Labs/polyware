using PolyWare.Core;
using System;
using System.Collections.Generic;

namespace PolyWare.Game {
	[Serializable]
	public class StatEvaluationEntry {
		public float Threshold;
		public float Value;
	}

	[Serializable]
	public class StatEvaluator : IEvaluator {
		public StatType Stat;
		public List<StatEvaluationEntry> StatValues;

		public float Evaluate(float input) {
			float statValue = StatValues[0].Value;

			foreach (StatEvaluationEntry statEvaluationEntry in StatValues) {
				if (statEvaluationEntry.Threshold > input) break;
				statValue = statEvaluationEntry.Value;
			}

			return statValue;
		}
	}
}