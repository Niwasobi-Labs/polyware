using System.Collections.Generic;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {

	
	public class Melee : Weapon {
		private CountdownTimer cooldownTimer;
		private Collider[] meleeResults;
		private LayerMask meleeLayerMask;

		private const string damageLayerName = "Damage";

		[ShowInInspector, ReadOnly]
		public MeleeData MeleeData => Data as MeleeData;
		
		protected override void OnInitialize() {
			meleeLayerMask = LayerMask.GetMask(damageLayerName);
			cooldownTimer = new CountdownTimer(MeleeData.Definition.MeleeInfo.Cooldown);
			meleeResults = new Collider[4];
		}

		public override bool CanUse => !cooldownTimer.IsRunning;

		public override void Use() {
			int hits = Physics.OverlapSphereNonAlloc(transform.position, MeleeData.Definition.MeleeInfo.Range, meleeResults, meleeLayerMask);

			if (hits == 0) return;

			for (int i = 0; i < hits; i++) {
				if (!meleeResults[i] || !meleeResults[i].TryGetComponent(out IDamageable damageable) || !meleeResults[i].TryGetComponent(out IAffectable affectable)) continue;
				
				if (damageable.GameObject.TryGetComponent(out IFactionMember factionMember) && !myCharacter.FactionMember.CanDamage(factionMember)) continue;

				DamageContext meleeDmgCtx = new DamageContext(MeleeData.MeleeDefinition.MeleeInfo.Damage.Element, MeleeData.MeleeDefinition.EvaluateMeleeDamage(myCharacter?.Stats, myCharacter?.Effects), myCharacter?.Transform.gameObject, MeleeData.MeleeDefinition.MeleeInfo.MeleeAbility);
					
				Ability abilityInstance = MeleeData.MeleeDefinition.MeleeInfo.MeleeAbility.CreateInstance();
				abilityInstance.Trigger(new AbilityContextHolder(
					abilityInstance.Definition, 
					myCharacter.Transform.gameObject,
					new List<GameObject> { affectable.GameObject },
					new List<IContext> {
						MeleeData,
						meleeDmgCtx
					})
				);
			}
			
			cooldownTimer.Start();
		}

		public override void StopUsing() {
			// noop
		}

		protected override void OnEquip() {
			// noop
		}

		protected override bool OnUnequip() {
			return true;
		}

		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying) return;
			
			Gizmos.DrawSphere(transform.position, MeleeData.Definition.MeleeInfo.Range);
		}
	}
}