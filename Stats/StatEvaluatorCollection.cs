using System;
using System.Collections.Generic;
using PolyWare.AssetManagement;
using UnityEngine;

namespace PolyWare.Stats {
	[CreateAssetMenu(fileName = "New StatEvaluatorCollection", menuName = "PolyWare/Collections/StatEvaluator Collection")]
	public class StatEvaluatorCollection : Collection {
		[SerializeField] protected List<StatEvaluator> data = new ();

		protected readonly Dictionary<StatType, StatEvaluator> evaluators = new();
		
		public override void Initialize() {
			evaluators.Clear();
			
			foreach (StatEvaluator value in data) {
				try {
					evaluators.Add(value.Stat, value);
				}
				catch (Exception e) {
					Debug.Log.Error(e.Message);
				}
			}
		}

		public bool Contains(StatType statType) {
			return evaluators.ContainsKey(statType);
		}
		
		public float Evaluate(StatType stat, float currentStat) {
			return evaluators[stat].Evaluate(currentStat);
		}
	}
}