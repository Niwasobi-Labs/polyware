using System;
using PolyWare.Stats;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	public class StatModifierPowerUp : PowerUp {
		public enum OperatorType {Add, Multiply}

		[SerializeField] private StatType statType = StatType.Attack;
		[SerializeField] private OperatorType operatorType =  OperatorType.Add;
		[SerializeField] private float value = 10f;
		[SerializeField] private float duration = 5f;

		private StatModifier modifier;

		protected override void OnInitialize() {
			base.OnInitialize();
			
			modifier = operatorType switch {
				OperatorType.Add => new BasicStatModifier(statType, duration, v => v + value),
				OperatorType.Multiply => new BasicStatModifier(statType, duration, v => v * value),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
		
		public override void OnUse() {
			if (character.Stats.AddModifier(modifier)) {
				Destroy(gameObject);
			}
		}
	}
}