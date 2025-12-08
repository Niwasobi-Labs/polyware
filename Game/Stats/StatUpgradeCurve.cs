using PolyWare.Core;
using System;
using System.Collections.Generic;

namespace PolyWare.Game {
	[Serializable]
	public class StatUpgradeCurveEntry {
		public float Threshold;
		public float Value;
	}

	[Serializable]
	public class StatUpgradeCurve : IEvaluator {
		public CharacterStatType BaseStat;
		public List<StatUpgradeCurveEntry> StatValues;

		public float Evaluate(float input) {
			float statValue = StatValues[0].Value;

			foreach (StatUpgradeCurveEntry statEvaluationEntry in StatValues) {
				if (statEvaluationEntry.Threshold > input) break;
				statValue = statEvaluationEntry.Value;
			}

			return statValue;
		}
	}
}