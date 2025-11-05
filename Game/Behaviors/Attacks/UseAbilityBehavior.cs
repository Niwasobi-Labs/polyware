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
		private float useCount;
		private readonly AbilityDefinition ability;

		private int uses;
		
		public UseAbilityBehavior(ICharacter character, UseAbilityBehaviorFactory factory) : base(character) {
			cooldownTimer = new CountdownTimer(factory.Cooldown);
			cooldownTimer.OnTimerComplete += UseAbility;
			useCount = factory.UseCount;
			ability = factory.Ability;
		}

		public override void Start() {
			uses = 0;
			cooldownTimer.Start();
		}
		
		private void UseAbility() {
			uses++;
			Ability newAbility = ability.CreateInstance();
			newAbility.Trigger(new AbilityContextHolder(ability, parent.GameObject,null, new List<IContext>{ parent.FactionMember.FactionInfo }));
			
			if (uses < useCount) cooldownTimer.Restart();
			else Complete();
		}
		
		public override void Tick(float dt) {
			cooldownTimer.Tick(dt);
		}

		public override void OnPlayerHit(ICharacter player) {
				
		}

		public override void Complete() {
			// no-op
		}
	}
}