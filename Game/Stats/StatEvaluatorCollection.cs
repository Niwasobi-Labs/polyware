using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(fileName = "New StatEvaluatorCollection", menuName = "PolyWare/Collections/StatEvaluator Collection")]
	public class StatEvaluatorCollection : Collection<StatType, StatEvaluator> {
		[SerializeField] protected List<StatEvaluator> data = new ();
		
		public override void Initialize() {
			dictionary.Clear();
			
			foreach (StatEvaluator value in data) {
				try {
					dictionary.Add(value.Stat, value);
				}
				catch (Exception e) {
					Log.Error(e.Message);
				}
			}
		}
		
		public float Evaluate(StatType stat, float currentStat) {
			return dictionary[stat].Evaluate(currentStat);
		}
	}
}