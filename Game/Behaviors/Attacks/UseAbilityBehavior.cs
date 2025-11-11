using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class UseAbilityBehaviorFactory : AttackBehaviorFactory {
		public int UseCount = -1;
		public float Cooldown = 1;
		public AbilityDefinition Ability;
		
		public override IBehavior Create(ICharacter character) {
			return new UseAbilityBehavior(character, this);
		}
	}
	
	public class UseAbilityBehavior : AttackBehavior {
		private readonly CountdownTimer cooldownTimer;
		private readonly int useCount;
		private readonly AbilityDefinition ability;
		
		private readonly ITelegrapher telegrapher;
		private readonly CountdownTimer castTimer;
		
		private int uses;
		
		public UseAbilityBehavior(ICharacter character, UseAbilityBehaviorFactory factory) : base(character) {
			cooldownTimer = new CountdownTimer(factory.Cooldown);
			cooldownTimer.OnTimerComplete += CastAbility;
			useCount = factory.UseCount;
			ability = factory.Ability;
			telegrapher = character.GameObject.GetComponent<ITelegrapher>();
			
			if (ability.CastTime > 0) {
				castTimer = new CountdownTimer(ability.CastTime);
				castTimer.OnTimerComplete += UseAbility;
			}
		}

		public override void Start() {
			uses = 0;
			cooldownTimer.Start();
		}

		private void CastAbility() {
			if (castTimer == null) {
				UseAbility();
				return;
			}
			
			castTimer.Start();
			telegrapher?.StartTelegraphing(ability.CastTime);
		}
		
		private void UseAbility() {
			telegrapher?.StopTelegraphing();
			uses++;
			Ability newAbility = ability.CreateInstance();
			newAbility.Trigger(new AbilityContextHolder(ability, parent.GameObject,null, new List<IContext>{ parent.FactionMember.FactionInfo }));
			
			if (uses < useCount || useCount == -1) cooldownTimer.Restart();
			else Complete();
		}
		
		public override void Tick(float dt) {
			cooldownTimer.Tick(dt);
			castTimer?.Tick(dt);
		}

		public override void OnPlayerHit(ICharacter player) {
				
		}

		public override void Complete() {
			// no-op
		}
	}
}