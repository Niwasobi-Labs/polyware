using System;
using System.Collections.Generic;

namespace PolyWare.Stats {
	[Serializable]
	public class StatEvaluationEntry {
		public float Threshold;
		public float Value;
	}
	
	[Serializable]
	public class StatEvaluator {
		public StatType Stat;
		public List<StatEvaluationEntry> StatValues;

		public float Evaluate(float currentStat) {
			float statValue = StatValues[0].Value;

			foreach (StatEvaluationEntry statEvaluationEntry in StatValues) {
				if (statEvaluationEntry.Threshold > currentStat) break;
				statValue = statEvaluationEntry.Value;
			}
			
			return statValue;
		}
	}
}