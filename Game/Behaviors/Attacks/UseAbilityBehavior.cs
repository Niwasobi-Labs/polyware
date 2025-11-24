using System;
using System.Collections.Generic;
using PolyWare.Core;

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
			
			uses = 0;
			
			if (ability.CastTime > 0) {
				castTimer = new CountdownTimer(ability.CastTime);
				castTimer.OnTimerComplete += UseAbility;
			}
		}

		protected override void OnStart() {
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
		
		protected override void OnTick(float dt) {
			cooldownTimer.Tick(dt);
			castTimer?.Tick(dt);
		}

		public override void OnPlayerHit(ICharacter player) {
			// no-op		
		}
		
		protected override void OnStun(bool isStunned) {
			if (isStunned) {
				castTimer?.Stop();
				telegrapher?.StopTelegraphing();
			}
			else {
				Start();
			}
		}
		
		protected override void OnComplete() {
			// no-op
		}
	}
}