using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(fileName = "New StatEvaluatorCollection", menuName = "PolyWare/Collections/StatUpgradeCurve Collection")]
	public class StatEvaluatorCollection : Collection<CharacterStatType, StatUpgradeCurve> {
		[SerializeField] protected List<StatUpgradeCurve> data = new ();
		
		public override void Initialize() {
			dictionary = new Dictionary<CharacterStatType, StatUpgradeCurve>();
			
			foreach (StatUpgradeCurve value in data) {
				try {
					dictionary.Add(value.BaseStat, value);
				}
				catch (Exception e) {
					Log.Error(e.Message);
				}
			}
		}
		
		public float Evaluate(CharacterStatType baseStat, float currentStat) {
			return dictionary[baseStat].Evaluate(currentStat);
		}
	}
}