using System;
using PolyWare.Characters;
using PolyWare.Stats;
using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	public class StatModifierPowerUp : PowerUp {
		public enum OperatorType {Add, Multiply}
		
		public override EntityData<PowerUpDefinition> Data { get; protected set; }

		[SerializeField] private StatType statType = StatType.Attack;
		[SerializeField] private OperatorType operatorType =  OperatorType.Add;
		[SerializeField] private float value = 10f;
		[SerializeField] private float duration = 5f;

		private StatModifier modifier;
		
		protected override void OnInitialize() {

		}
		
		public override void OnUse() {
			// todo: should move to OnInitialize, but would require spawning this in, not manual placement
			modifier = operatorType switch {
				OperatorType.Add => new BasicStatModifier(statType, duration, v => v + value),
				OperatorType.Multiply => new BasicStatModifier(statType, duration, v => v * value),
				_ => throw new ArgumentOutOfRangeException()
			};
			
			if (character.Stats.AddModifier(modifier)) {
				Destroy(gameObject);
			}
		}
	}
}