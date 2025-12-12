using System;
using System.Collections.Generic;
using PolyWare.Core;

namespace PolyWare.Game {
	[Serializable]
	public class UseAbilityBehaviorFactory : AttackBehaviorFactory {
		public AbilityDefinition Ability;
		
		public override IBehavior Create(ICharacter character) {
			return new UseAbilityBehavior(character, this);
		}
	}
	
	public class UseAbilityBehavior : AttackBehavior {
		private readonly AbilityDefinition ability;
		
		public UseAbilityBehavior(ICharacter character, UseAbilityBehaviorFactory factory) : base(character, factory) {
			ability = factory.Ability;
		}

		protected override void OnStart() {
			UseAbility();
		}
		
		private void UseAbility() {
			Ability newAbility = ability.CreateInstance();
			newAbility.Trigger(new AbilityContextHolder(ability, parent.GameObject,null, new List<IContext>{ parent.FactionMember.FactionInfo }));

			Complete();
		}
		
		protected override void OnTick(float dt) {
			// no-op
		}

		public override void OnPlayerHit(ICharacter player) {
			// no-op		
		}
		
		protected override void OnStun(bool isStunned) {
			// no-op
		}
		
		protected override void OnComplete() {
			// no-op
		}
	}
}