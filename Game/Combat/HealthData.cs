using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public struct HealthData {
		public float InitialMaxHealth;
		[SerializeReference] private IStatEvaluator maxHealthEvaluator;
		[ReadOnly] public float Current;
		public bool Invincible;
		public bool HasDamageCooldown;
		[ShowIf("HasDamageCooldown")] public float DamageCooldownDuration;
		public bool CanHeal;
		public bool FinalStand;
		public float Overcharge;
			
		public bool CanRegen;
		[ShowIf("CanRegen")] public float RegenDelayTime;
		[ShowIf("CanRegen")] public float RegenTime;
		
		public bool CanBeStunned;
		[ShowIf("CanBeStunned")] public float StunThreshold;
		[ShowIf("CanBeStunned")] public bool StunOnDeath;
		[ShowIf("CanBeStunned")] public float StunDuration;
		[ShowIf("CanBeStunned")] public float StunRecoveryHealth;

		public float MaxHealth(IStatsHandler statsHandler = null, IEffectsHandler effects = null) {
			return maxHealthEvaluator?.Evaluate(statsHandler, effects, InitialMaxHealth) ?? InitialMaxHealth;
		}
	}
}